namespace ACME.BankingPlatform.API.Clients.Domain.Events;

public record ClientRegistered(long Id, string FirstName, string LastName, string Dni) : IEvent;
