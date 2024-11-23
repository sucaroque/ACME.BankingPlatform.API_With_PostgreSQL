using ACME.BankingPlatform.API.Accounts.Domain.Events;

namespace ACME.BankingPlatform.API.Notifications.Interfaces.EventHandlers;

public class AccountOpenedEventHandler : IHandleMessages<AccountOpened>
{
    public Task Handle(AccountOpened @event, IMessageHandlerContext context)
    {
        Console.WriteLine("Send Notification - AccountOpenedEventHandler");
        return Task.CompletedTask;
    }
}
