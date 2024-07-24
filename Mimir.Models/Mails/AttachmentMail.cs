using Bencodex.Types;
using Mimir.Models.AttachmentActionResults;
using Mimir.Models.Exceptions;
using Mimir.Models.Factories;
using ValueKind = Bencodex.Types.ValueKind;

namespace Mimir.Models.Mails;

/// <summary>
/// <see cref="Nekoyume.Model.Mail.AttachmentMail"/>
/// </summary>
public record AttachmentMail : Mail
{
    public AttachmentActionResult Attachment { get; init; }

    public override IValue Bencoded => ((Dictionary)base.Bencoded)
        .Add("attachment", Attachment.Bencoded);

    public AttachmentMail(IValue bencoded) : base(bencoded)
    {
        if (bencoded is not Dictionary d)
        {
            throw new UnsupportedArgumentTypeException<ValueKind>(
                nameof(bencoded),
                [ValueKind.Dictionary],
                bencoded.Kind);
        }

        Attachment = AttachmentActionResultFactory.Create(d["attachment"]);
    }
}
