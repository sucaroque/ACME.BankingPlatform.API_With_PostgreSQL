namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record DepositMarkedAsCompleted(long TransactionId) : IEvent;
