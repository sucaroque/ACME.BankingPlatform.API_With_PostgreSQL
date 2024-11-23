namespace ACME.BankingPlatform.API.Accounts.Application.Commands;

public record CreditAccount(long TransactionId, long AccountId, decimal Amount) : ICommand;
