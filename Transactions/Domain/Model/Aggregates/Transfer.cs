using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;
using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Domain.Events;
using ACME.BankingPlatform.API.Transactions.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;

public class Transfer : Transaction
{
    public long ToAccountId { get; protected set; }

    public Transfer() : base()
    {
    }
    
    public void Start(StartTransfer command)
    {
        Id = command.TransactionId;
        FromAccountId = command.FromAccountId;
        ToAccountId = command.ToAccountId;
        Amount = Money.Dollars(command.Amount);
        State = ETransactionState.STARTED;
        Type = ETransactionType.TRANSFER;
        var @event = new TransferStarted(command.TransactionId, command.FromAccountId, command.ToAccountId, command.Amount);
        AddDomainEvent(@event);
    }
    
    public void MarkAsFailed(MarkTransferAsFailed command)
    {
        State = ETransactionState.FAILED;
        var @event = new TransferMarkedAsFailed(command.TransactionId);
        AddDomainEvent(@event);
    }
    
    public void MarkAsCompleted(MarkTransferAsCompleted command)
    {
        State = ETransactionState.COMPLETED;
        var @event = new TransferMarkedAsCompleted(command.TransactionId);
        AddDomainEvent(@event);
    }
}
