using ACME.BankingPlatform.API.Accounts.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;

namespace ACME.BankingPlatform.API.Accounts.Application.Commands.Handlers;

public class CreditAccountCommandHandler(IAccountRepository repository, IUnitOfWork unitOfWork)
    : IHandleMessages<CreditAccount>
{
    public async Task Handle(CreditAccount command, IMessageHandlerContext context)
    {
        var account = await repository.FindByIdAsync(command.AccountId);
        if (account is null) return;
        account.Credit(command);
        repository.Update(account);
        await unitOfWork.CompleteAsync(account, context);
    }
}
