using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Handlers;

public class MarkWithdrawalAsCompletedCommandHandler(ITransactionRepository<Withdrawal> repository, IUnitOfWork unitOfWork)
    : IHandleMessages<MarkWithdrawalAsCompleted>
{
    public async Task Handle(MarkWithdrawalAsCompleted command, IMessageHandlerContext context)
    {
        var withdrawal = await repository.FindByIdAsync(command.TransactionId);
        if (withdrawal is null) return;
        withdrawal.MarkAsCompleted(command);
        repository.Update(withdrawal);
        await unitOfWork.CompleteAsync(withdrawal, context);
    }
}
