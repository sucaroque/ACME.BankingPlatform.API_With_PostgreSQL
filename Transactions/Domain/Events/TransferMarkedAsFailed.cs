namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record TransferMarkedAsFailed(long TransactionId) : IEvent;
