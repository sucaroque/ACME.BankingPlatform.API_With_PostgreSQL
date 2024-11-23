using ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Interfaces.REST;
using MediatR;

namespace ACME.BankingPlatform.API.Clients.Application.Queries;

public record GetClients(int Page, int Limit) : IRequest<(IEnumerable<Client>, PaginationMetadata)>;
