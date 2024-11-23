using ACME.BankingPlatform.API.Accounts.Interfaces.ACL.Services;

namespace ACME.BankingPlatform.API.Transactions.Application.OutboundServices.ACL;

public class ExternalAccountsContextService(AccountsContextFacade accountsContextFacade)
{
    public async Task<bool> ExistsAccountById(long id)
    {
        return await accountsContextFacade.ExistsAccountById(id);
    }
}
