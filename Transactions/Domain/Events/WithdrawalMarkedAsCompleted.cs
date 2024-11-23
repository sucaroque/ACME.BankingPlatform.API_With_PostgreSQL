namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record WithdrawalMarkedAsCompleted(long TransactionId) : IEvent;
