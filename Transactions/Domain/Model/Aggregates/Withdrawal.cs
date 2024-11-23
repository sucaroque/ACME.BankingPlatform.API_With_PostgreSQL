using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;
using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Domain.Events;
using ACME.BankingPlatform.API.Transactions.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;

public class Withdrawal : Transaction
{
    public Withdrawal() : base()
    {
    }

    public Withdrawal(long id, Money amount, long accountId)
        : base(id, amount, accountId, ETransactionState.STARTED, ETransactionType.WITHDRAWAL)
    {
    }
    
    public void Start(StartWithdrawal command)
    {
        Id = command.TransactionId;
        FromAccountId = command.AccountId;
        Amount = Money.Dollars(command.Amount);
        State = ETransactionState.STARTED;
        Type = ETransactionType.WITHDRAWAL;
        var @event = new WithdrawalStarted(command.TransactionId, command.AccountId, command.Amount);
        AddDomainEvent(@event);
    }
    
    public void MarkAsFailed(MarkWithdrawalAsFailed command)
    {
        State = ETransactionState.FAILED;
        var @event = new WithdrawalMarkedAsFailed(command.TransactionId);
        AddDomainEvent(@event);
    }
    
    public void MarkAsCompleted(MarkWithdrawalAsCompleted command)
    {
        State = ETransactionState.COMPLETED;
        var @event = new WithdrawalMarkedAsCompleted(command.TransactionId);
        AddDomainEvent(@event);
    }
}
