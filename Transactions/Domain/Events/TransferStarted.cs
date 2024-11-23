namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record TransferStarted(long TransactionId, long FromAccountId, long ToAccountId, decimal Amount) : IEvent;
