namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record AccountDebited(long AccountId, long TransactionId, decimal Amount) : IEvent;
