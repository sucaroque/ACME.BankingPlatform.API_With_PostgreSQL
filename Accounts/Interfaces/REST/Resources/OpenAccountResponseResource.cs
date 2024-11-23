using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Accounts.Interfaces.REST.Resources;

public record OpenAccountResponseResource(AccountResource? Success, List<Notification.Error>? Errors);
