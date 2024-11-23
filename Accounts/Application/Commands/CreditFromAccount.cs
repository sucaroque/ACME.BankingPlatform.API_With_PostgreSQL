namespace ACME.BankingPlatform.API.Accounts.Application.Commands;

public record CreditFromAccount(long TransactionId, long AccountId, decimal Amount) : ICommand;
