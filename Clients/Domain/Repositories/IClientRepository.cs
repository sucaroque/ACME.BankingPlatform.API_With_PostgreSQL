using ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Interfaces.REST;

namespace ACME.BankingPlatform.API.Clients.Domain.Repositories;

public interface IClientRepository : IBaseRepository<Client>
{
    Task<Client?> FindByDniAsync(string dni);
    public Task<(IEnumerable<Client>, PaginationMetadata)> GetPaginatedAsync(int page, int limit);
}
