namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record WithdrawalStarted(long TransactionId, long AccountId, decimal Amount) : IEvent;
