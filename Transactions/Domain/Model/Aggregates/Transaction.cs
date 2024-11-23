using ACME.BankingPlatform.API.Shared.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;
using ACME.BankingPlatform.API.Transactions.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;

public abstract partial class Transaction(
    long id,
    Money amount,
    long fromAccountId,
    ETransactionState state,
    ETransactionType type) : AggregateRoot
{
    public long Id { get; protected set; } = id;
    public Money Amount { get; protected set; } = amount;
    public long FromAccountId { get; protected set; } = fromAccountId;
    public ETransactionState State { get; protected set; } = state;
    public ETransactionType Type { get; protected set; } = type;

    protected Transaction() : this(0, new Money(), 0, ETransactionState.FAILED, (ETransactionType)0)
    {
    }
}
