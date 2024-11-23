using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Accounts.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;

namespace ACME.BankingPlatform.API.Accounts.Application.Commands.Handlers;

public class OpenAccountCommandHandler(IAccountRepository repository, IUnitOfWork unitOfWork)
    : IHandleMessages<OpenAccount>
{
    public async Task Handle(OpenAccount command, IMessageHandlerContext context)
    {
        var account = new Account();
        account.Open(command);
        await repository.AddAsync(account);
        await unitOfWork.CompleteAsync(account, context);
    }
}
