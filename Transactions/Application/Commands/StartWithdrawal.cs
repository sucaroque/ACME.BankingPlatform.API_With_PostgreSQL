namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record StartWithdrawal(long TransactionId, long AccountId, decimal Amount) : ICommand;
