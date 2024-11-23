using ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Clients.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Interfaces.REST;
using MediatR;

namespace ACME.BankingPlatform.API.Clients.Application.Queries.Handlers;

public class GetClientsQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetClients, (IEnumerable<Client>, PaginationMetadata)>
{
    public async Task<(IEnumerable<Client>, PaginationMetadata)> Handle(GetClients query, CancellationToken cancellationToken)
    {
        return await clientRepository.GetPaginatedAsync(query.Page, query.Limit);
    }
}
