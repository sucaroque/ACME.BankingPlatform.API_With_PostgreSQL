using ACME.BankingPlatform.API.Clients.Application.Commands.Validators;
using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Clients.Application.Commands.Services;

public class ClientCommandService(IMessageSession messageSession, RegisterClientValidator depositValidator)
{
    public async Task<Notification> Register(RegisterClient command)
    {
        var notification = await depositValidator.Validate(command);
        if (notification.HasErrors) return notification;
        await messageSession.Send(command).ConfigureAwait(false);
        return notification;
    }
}
