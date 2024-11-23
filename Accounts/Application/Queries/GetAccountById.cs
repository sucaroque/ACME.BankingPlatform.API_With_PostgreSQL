using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using MediatR;

namespace ACME.BankingPlatform.API.Accounts.Application.Queries;

public record GetAccountById(long AccountId) : IRequest<Account?>;
