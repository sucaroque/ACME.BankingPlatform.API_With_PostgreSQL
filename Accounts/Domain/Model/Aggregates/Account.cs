using ACME.BankingPlatform.API.Accounts.Application.Commands;
using ACME.BankingPlatform.API.Accounts.Domain.Events;
using ACME.BankingPlatform.API.Shared.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;

public partial class Account(long id, string number, Money balance, long clientId) : AggregateRoot
{
    public long Id { get; private set; } = id;
    public string Number { get; private set; } = number;
    public Money Balance { get; private set; } = balance;
    public long ClientId { get; private set; } = clientId;

    public Account() : this(0, "", new Money(), 0)
    {
    }
    
    public void Open(OpenAccount command)
    {
        Id = command.Id;
        Number = command.Number;
        Balance = Money.Dollars(0);
        ClientId = command.ClientId;        
        var @event = new AccountOpened(command.Id, command.Number, command.OverdraftLimit, command.ClientId);
        AddDomainEvent(@event);
    }
    
    public void Credit(CreditAccount command)
    {
        var notification = new Notification();
        if (command.Amount <= 0) notification.AddError("The amount must be greater than zero");
        if (notification.HasErrors)
        {
            AddDomainEvent(new AccountInvalidDataFound(command.AccountId, command.TransactionId, notification.Errors.ToList()));
            return;
        }
        var amount = Money.Dollars(command.Amount);
        Balance = Balance.Add(amount);
        var @event = new AccountCredited(command.AccountId, command.TransactionId, command.Amount);
        AddDomainEvent(@event);
    }
    
    public void Debit(DebitAccount command)
    {
        var notification = new Notification();
        if (command.Amount <= 0) notification.AddError("The amount must be greater than zero");
        if (notification.HasErrors)
        {
            AddDomainEvent(new AccountInvalidDataFound(command.AccountId, command.TransactionId, notification.Errors.ToList()));
            return;
        }
        if (Balance.Amount < command.Amount)
        {
            AddDomainEvent(new InsufficientFundsDetected(command.AccountId, command.TransactionId));
            return;
        }
        var amount = Money.Dollars(command.Amount);
        Balance = Balance.Subtract(amount);
        var @event = new AccountDebited(command.AccountId, command.TransactionId, command.Amount);
        AddDomainEvent(@event);
    }
    
    public void CreditFromAccount(CreditFromAccount command)
    {
        var notification = new Notification();
        if (command.Amount <= 0) notification.AddError("The amount must be greater than zero");
        if (notification.HasErrors)
        {
            AddDomainEvent(new AccountInvalidDataFound(command.AccountId, command.TransactionId, notification.Errors.ToList()));
            return;
        }
        var amount = Money.Dollars(command.Amount);
        Balance = Balance.Add(amount);
        var @event = new FromAccountCredited(command.AccountId, command.TransactionId, command.Amount);
        AddDomainEvent(@event);
    }
}
