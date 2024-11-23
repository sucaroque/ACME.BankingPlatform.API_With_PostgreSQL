using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;

namespace ACME.BankingPlatform.API.Accounts.Domain.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Account?> FindByNumberAsync(string number);
}
