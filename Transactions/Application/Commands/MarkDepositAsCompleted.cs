namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record MarkDepositAsCompleted(long TransactionId) : ICommand;
