using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Handlers;

public class MarkTransferAsFailedCommandHandler(ITransactionRepository<Transfer> repository, IUnitOfWork unitOfWork)
    : IHandleMessages<MarkTransferAsFailed>
{
    public async Task Handle(MarkTransferAsFailed command, IMessageHandlerContext context)
    {
        var transfer = await repository.FindByIdAsync(command.TransactionId);
        if (transfer is null) return;
        transfer.MarkAsFailed(command);
        repository.Update(transfer);
        await unitOfWork.CompleteAsync(transfer, context);
    }
}
