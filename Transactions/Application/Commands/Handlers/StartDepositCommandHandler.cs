using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Handlers;

public class StartDepositCommandHandler(ITransactionRepository<Deposit> repository, IUnitOfWork unitOfWork)
    : IHandleMessages<StartDeposit>
{
    public async Task Handle(StartDeposit command, IMessageHandlerContext context)
    {
        var deposit = new Deposit();
        deposit.Start(command);
        await repository.AddAsync(deposit);
        await unitOfWork.CompleteAsync(deposit, context);
    }
}
