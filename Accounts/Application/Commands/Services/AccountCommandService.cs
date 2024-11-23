using ACME.BankingPlatform.API.Accounts.Application.Commands.Validators;
using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Accounts.Application.Commands.Services;

public class AccountCommandService(IMessageSession messageSession, OpenAccountValidator openAccountValidator)
{
    public async Task<Notification> OpenAccount(OpenAccount command)
    {
        var notification = await openAccountValidator.Validate(command);
        if (notification.HasErrors) return notification;
        await messageSession.Send(command).ConfigureAwait(false);
        return notification;
    }
}
