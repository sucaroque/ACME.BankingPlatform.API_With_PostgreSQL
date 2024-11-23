namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record AccountOpened(long Id, string Number, decimal OverdraftLimit, long ClientId) : IEvent;
