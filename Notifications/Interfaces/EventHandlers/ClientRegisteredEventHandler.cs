using ACME.BankingPlatform.API.Clients.Domain.Events;

namespace ACME.BankingPlatform.API.Notifications.Interfaces.EventHandlers;

public class ClientRegisteredEventHandler : IHandleMessages<ClientRegistered>
{
    public Task Handle(ClientRegistered @event, IMessageHandlerContext context)
    {
        Console.WriteLine("Send Notification - ClientRegisteredHandler");
        return Task.CompletedTask;
    }
}
