using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using MediatR;

namespace ACME.BankingPlatform.API.Accounts.Application.Queries.Services;

public class AccountQueryService(ISender sender)
{
    public async Task<Account?> GetAccountById(long accountId)
    {
        var query = new GetAccountById(accountId);
        return await sender.Send(query);
    }
}
