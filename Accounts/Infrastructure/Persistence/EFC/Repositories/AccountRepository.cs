using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Accounts.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACME.BankingPlatform.API.Accounts.Infrastructure.Persistence.EFC.Repositories;

public class AccountRepository(AppDbContext context) : BaseRepository<Account>(context), IAccountRepository
{
    public Task<Account?> FindByNumberAsync(string number)
    {
        return Context.Set<Account>().Where(p => p.Number == number).FirstOrDefaultAsync();
    }
}
