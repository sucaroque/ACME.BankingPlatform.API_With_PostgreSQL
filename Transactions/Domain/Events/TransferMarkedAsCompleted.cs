namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record TransferMarkedAsCompleted(long TransactionId) : IEvent;
