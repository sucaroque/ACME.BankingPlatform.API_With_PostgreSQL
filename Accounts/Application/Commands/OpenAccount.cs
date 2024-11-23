namespace ACME.BankingPlatform.API.Accounts.Application.Commands;

public record OpenAccount(long Id, string Number, decimal OverdraftLimit, long ClientId) : ICommand;
