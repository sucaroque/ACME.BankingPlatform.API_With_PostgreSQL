using ACME.BankingPlatform.API.Accounts.Domain.Events;

namespace ACME.BankingPlatform.API.Accounts.Interfaces.EventHandlers;

public class AccountNotFoundEventHandler : IHandleMessages<AccountNotFound>
{
    public Task Handle(AccountNotFound @event, IMessageHandlerContext context)
    {
        Console.WriteLine("Code here - AccountNotFoundEventHandler");
        return Task.CompletedTask;
    }
}
