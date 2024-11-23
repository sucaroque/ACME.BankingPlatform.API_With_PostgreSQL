using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Handlers;

public class StartTransferCommandHandler(ITransactionRepository<Transfer> repository, IUnitOfWork unitOfWork)
    : IHandleMessages<StartTransfer>
{
    public async Task Handle(StartTransfer command, IMessageHandlerContext context)
    {
        var transfer = new Transfer();
        transfer.Start(command);
        await repository.AddAsync(transfer);
        await unitOfWork.CompleteAsync(transfer, context);
    }
}
