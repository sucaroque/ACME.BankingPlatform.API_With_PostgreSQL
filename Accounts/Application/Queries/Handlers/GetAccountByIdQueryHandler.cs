using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Accounts.Domain.Repositories;
using MediatR;

namespace ACME.BankingPlatform.API.Accounts.Application.Queries.Handlers;

public class GetAccountByIdQueryHandler(IAccountRepository clientRepository) : IRequestHandler<GetAccountById, Account?>
{
    public async Task<Account?> Handle(GetAccountById query, CancellationToken cancellationToken)
    {
        return await clientRepository.FindByIdAsync(query.AccountId);
    }
}
