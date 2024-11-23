using ACME.BankingPlatform.API.Accounts.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;

namespace ACME.BankingPlatform.API.Accounts.Application.Commands.Handlers;

public class DebitAccountCommandHandler(IAccountRepository repository, IUnitOfWork unitOfWork)
    : IHandleMessages<DebitAccount>
{
    public async Task Handle(DebitAccount command, IMessageHandlerContext context)
    {
        var account = await repository.FindByIdAsync(command.AccountId);
        if (account is null) return;
        account.Debit(command);
        repository.Update(account);
        await unitOfWork.CompleteAsync(account, context);
    }
}
