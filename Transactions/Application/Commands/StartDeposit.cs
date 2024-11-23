namespace ACME.BankingPlatform.API.Transactions.Application.Commands;

public record StartDeposit(long TransactionId, long AccountId, decimal Amount) : ICommand;
