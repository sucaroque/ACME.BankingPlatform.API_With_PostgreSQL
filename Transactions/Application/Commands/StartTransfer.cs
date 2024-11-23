namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record StartTransfer(long TransactionId, long FromAccountId, long ToAccountId, decimal Amount) : ICommand;
