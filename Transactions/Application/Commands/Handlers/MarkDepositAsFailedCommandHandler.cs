using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Handlers;

public class MarkDepositAsFailedCommandHandler(ITransactionRepository<Deposit> repository, IUnitOfWork unitOfWork)
    : IHandleMessages<MarkDepositAsFailed>
{
    public async Task Handle(MarkDepositAsFailed command, IMessageHandlerContext context)
    {
        var deposit = await repository.FindByIdAsync(command.TransactionId);
        if (deposit is null) return;
        deposit.MarkAsFailed(command);
        repository.Update(deposit);
        await unitOfWork.CompleteAsync(deposit, context);
    }
}
