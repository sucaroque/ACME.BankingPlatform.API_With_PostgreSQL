namespace ACME.BankingPlatform.API.Clients.Application.Commands;

public record RegisterClient(long Id, string FirstName, string LastName, string Dni) : ICommand;
