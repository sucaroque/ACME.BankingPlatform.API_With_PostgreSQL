namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record FromAccountCredited(long AccountId, long TransactionId, decimal Amount) : IEvent;
