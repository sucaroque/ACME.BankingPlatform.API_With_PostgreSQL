namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record ToAccountNotFound(long TransactionId) : IEvent;
