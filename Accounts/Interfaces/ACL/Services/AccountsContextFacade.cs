using ACME.BankingPlatform.API.Accounts.Application.Queries.Services;

namespace ACME.BankingPlatform.API.Accounts.Interfaces.ACL.Services;

public class AccountsContextFacade(AccountQueryService accountQueryService)
{
    public async Task<bool> ExistsAccountById(long id)
    {
        var account = await accountQueryService.GetAccountById(id);
        return account != null;
    }
}
