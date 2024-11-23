namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record AccountNotFound(long TransactionId) : IEvent;
