namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record MarkTransferAsCompleted(long TransactionId) : ICommand;
