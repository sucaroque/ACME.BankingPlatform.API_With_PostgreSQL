using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Handlers;

public class MarkDepositAsCompletedCommandHandler(ITransactionRepository<Deposit> repository, IUnitOfWork unitOfWork)
    : IHandleMessages<MarkDepositAsCompleted>
{
    public async Task Handle(MarkDepositAsCompleted command, IMessageHandlerContext context)
    {
        var deposit = await repository.FindByIdAsync(command.TransactionId);
        if (deposit is null) return;
        deposit.MarkAsCompleted(command);
        repository.Update(deposit);
        await unitOfWork.CompleteAsync(deposit, context);
    }
}
