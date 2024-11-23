namespace ACME.BankingPlatform.API.Transactions.Domain.Events;

public record WithdrawalMarkedAsFailed(long TransactionId) : IEvent;
