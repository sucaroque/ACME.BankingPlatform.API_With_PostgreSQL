namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record MarkDepositAsFailed(long TransactionId) : ICommand;
