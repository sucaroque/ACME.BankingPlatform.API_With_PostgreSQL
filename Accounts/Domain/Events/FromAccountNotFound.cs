namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record FromAccountNotFound(long TransactionId) : IEvent;
