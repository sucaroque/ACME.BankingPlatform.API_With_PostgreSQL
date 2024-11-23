namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record MarkWithdrawalAsFailed(long TransactionId) : ICommand;
