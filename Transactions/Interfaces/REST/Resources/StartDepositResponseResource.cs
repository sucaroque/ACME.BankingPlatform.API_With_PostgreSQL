using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

public record StartDepositResponseResource(StartDepositResource? Success, List<Notification.Error>? Errors
) {}
