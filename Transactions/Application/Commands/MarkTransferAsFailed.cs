namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record MarkTransferAsFailed(long TransactionId) : ICommand;
