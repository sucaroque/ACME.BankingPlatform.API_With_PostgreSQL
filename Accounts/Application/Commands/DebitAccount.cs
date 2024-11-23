namespace ACME.BankingPlatform.API.Accounts.Application.Commands;

public record DebitAccount(long TransactionId, long AccountId, decimal Amount) : ICommand;
