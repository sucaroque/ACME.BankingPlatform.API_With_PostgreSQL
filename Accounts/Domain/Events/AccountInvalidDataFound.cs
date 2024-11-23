using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Accounts.Domain.Events;

public record AccountInvalidDataFound(long AccountId, long TransactionId, List<Notification.Error> Errors) : IEvent;
