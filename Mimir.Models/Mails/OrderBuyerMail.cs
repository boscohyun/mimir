using Bencodex.Types;
using Mimir.Models.Exceptions;
using Nekoyume.Model.State;
using ValueKind = Bencodex.Types.ValueKind;
using static Lib9c.SerializeKeys;

namespace Mimir.Models.Mails;

/// <summary>
/// <see cref="Nekoyume.Model.Mail.OrderBuyerMail"/>
/// </summary>
public record OrderBuyerMail : Mail
{
    public Guid OrderId { get; init; }

    public override IValue Bencoded => ((Dictionary)base.Bencoded)
        .Add(OrderIdKey, OrderId.Serialize());

    public OrderBuyerMail(IValue bencoded) : base(bencoded)
    {
        if (bencoded is not Dictionary d)
        {
            throw new UnsupportedArgumentTypeException<ValueKind>(
                nameof(bencoded),
                [ValueKind.Dictionary],
                bencoded.Kind);
        }

        OrderId = d[OrderIdKey].ToGuid();
    }
}
