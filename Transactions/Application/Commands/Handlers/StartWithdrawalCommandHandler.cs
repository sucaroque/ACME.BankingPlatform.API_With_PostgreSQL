using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Handlers;

public class StartWithdrawalCommandHandler(ITransactionRepository<Withdrawal> repository, IUnitOfWork unitOfWork)
    : IHandleMessages<StartWithdrawal>
{
    public async Task Handle(StartWithdrawal command, IMessageHandlerContext context)
    {
        var withdrawal = new Withdrawal();
        withdrawal.Start(command);
        await repository.AddAsync(withdrawal);
        await unitOfWork.CompleteAsync(withdrawal, context);
    }
}
