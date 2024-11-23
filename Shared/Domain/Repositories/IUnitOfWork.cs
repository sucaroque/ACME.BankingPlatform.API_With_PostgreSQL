using ACME.BankingPlatform.API.Shared.Domain.Model.Aggregates;

namespace ACME.BankingPlatform.API.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync(AggregateRoot aggregateRoot, IMessageHandlerContext messageHandlerContext);
}
