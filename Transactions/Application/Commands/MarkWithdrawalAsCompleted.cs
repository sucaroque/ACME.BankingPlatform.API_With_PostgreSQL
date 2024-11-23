namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record MarkWithdrawalAsCompleted(long TransactionId) : ICommand;
