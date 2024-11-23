using ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Clients.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using ACME.BankingPlatform.API.Shared.Interfaces.REST;
using Microsoft.EntityFrameworkCore;

namespace ACME.BankingPlatform.API.Clients.Infrastructure.Persistence.EFC.Repositories;

public class ClientRepository(AppDbContext context) : BaseRepository<Client>(context), IClientRepository
{
    public async Task<Client?> FindByDniAsync(string dni)
    {
        return await Context.Set<Client>().Where(p => p.Dni == dni).FirstOrDefaultAsync();
    }
    
    public async Task<(IEnumerable<Client>, PaginationMetadata)> GetPaginatedAsync(int page, int limit)
    {
        IQueryable<Client> collection = Context.Set<Client>();
        var totalItemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetadata(totalItemCount, limit, page);

        var startIndex = limit * (page - 1);
        
        var collectionToReturn = await collection.OrderBy(c => c.Name.LastName)
            .Skip(startIndex)
            .AsNoTracking()
            .Take(limit)
            .ToListAsync();

        return (collectionToReturn, paginationMetadata);
    }
}
