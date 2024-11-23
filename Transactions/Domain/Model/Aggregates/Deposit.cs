using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;
using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Domain.Events;
using ACME.BankingPlatform.API.Transactions.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;

public class Deposit : Transaction
{
    public Deposit() : base()
    {
    }
    
    public void Start(StartDeposit command)
    {
        Id = command.TransactionId;
        FromAccountId = command.AccountId;
        Amount = Money.Dollars(command.Amount);
        State = ETransactionState.STARTED;
        Type = ETransactionType.DEPOSIT;
        var @event = new DepositStarted(command.TransactionId, command.AccountId, command.Amount);
        AddDomainEvent(@event);
    }
    
    public void MarkAsFailed(MarkDepositAsFailed command)
    {
        State = ETransactionState.FAILED;
        var @event = new DepositMarkedAsFailed(command.TransactionId);
        AddDomainEvent(@event);
    }
    
    public void MarkAsCompleted(MarkDepositAsCompleted command)
    {
        State = ETransactionState.COMPLETED;
        var @event = new DepositMarkedAsCompleted(command.TransactionId);
        AddDomainEvent(@event);
    }
}
