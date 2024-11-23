namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record AccountCredited(long AccountId, long TransactionId, decimal Amount) : IEvent;
