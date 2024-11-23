using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Clients.Interfaces.REST.Resources;

public record RegisterClientResponseResource(ClientResource? Success, List<Notification.Error>? Errors);
