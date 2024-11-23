using ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Clients.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;

namespace ACME.BankingPlatform.API.Clients.Application.Commands.Handlers;

public class RegisterClientCommandHandler(IClientRepository repository, IUnitOfWork unitOfWork)
    : IHandleMessages<RegisterClient>
{
    public async Task Handle(RegisterClient command, IMessageHandlerContext context)
    {
        var client = new Client();
        client.Register(command);
        await repository.AddAsync(client);
        await unitOfWork.CompleteAsync(client, context);
    }
}
