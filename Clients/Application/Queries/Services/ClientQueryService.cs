using ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Interfaces.REST;
using MediatR;

namespace ACME.BankingPlatform.API.Clients.Application.Queries.Services;

public class ClientQueryService(ISender sender)
{
    public async Task<(IEnumerable<Client>, PaginationMetadata)> GetClients(int page, int limit)
    {
        var query = new GetClients(page, limit); 
        return await sender.Send(query);
    }
}
