using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Handlers;

public class MarkWithdrawalAsFailedCommandHandler(ITransactionRepository<Withdrawal> repository, IUnitOfWork unitOfWork)
    : IHandleMessages<MarkWithdrawalAsFailed>
{
    public async Task Handle(MarkWithdrawalAsFailed command, IMessageHandlerContext context)
    {
        var withdrawal = await repository.FindByIdAsync(command.TransactionId);
        if (withdrawal is null) return;
        withdrawal.MarkAsFailed(command);
        repository.Update(withdrawal);
        await unitOfWork.CompleteAsync(withdrawal, context);
    }
}
