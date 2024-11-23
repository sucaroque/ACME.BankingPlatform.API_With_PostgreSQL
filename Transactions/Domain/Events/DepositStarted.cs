namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record DepositStarted(long TransactionId, long AccountId, decimal Amount) : IEvent;
