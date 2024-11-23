namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record DepositMarkedAsFailed(long TransactionId) : IEvent;
