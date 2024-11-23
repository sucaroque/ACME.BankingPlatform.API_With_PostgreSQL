namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record InsufficientFundsDetected(long AccountId, long TransactionId) : IEvent;
