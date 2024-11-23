using ACME.BankingPlatform.API.Accounts.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;

namespace ACME.BankingPlatform.API.Accounts.Application.Commands.Handlers;

public class CreditFromAccountCommandHandler(IAccountRepository repository, IUnitOfWork unitOfWork)
    : IHandleMessages<CreditFromAccount>
{
    public async Task Handle(CreditFromAccount command, IMessageHandlerContext context)
    {
        var account = await repository.FindByIdAsync(command.AccountId);
        if (account is null) return;
        account.CreditFromAccount(command);
        repository.Update(account);
        await unitOfWork.CompleteAsync(account, context);
    }
}
