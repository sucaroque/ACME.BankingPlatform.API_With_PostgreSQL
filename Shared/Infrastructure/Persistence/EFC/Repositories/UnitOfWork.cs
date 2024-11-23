using ACME.BankingPlatform.API.Shared.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task CompleteAsync(AggregateRoot aggregateRoot, IMessageHandlerContext messageHandlerContext)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(messageHandlerContext.CancellationToken);
        try
        {
            await context.SaveChangesAsync(messageHandlerContext.CancellationToken);
            await PublishDomainEventsAsync(aggregateRoot, messageHandlerContext);
            await transaction.CommitAsync(messageHandlerContext.CancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(messageHandlerContext.CancellationToken);
            throw new Exception("Error occurred while completing transaction.", ex);
        }
    }
    
    private static async Task PublishDomainEventsAsync(AggregateRoot aggregateRoot, IMessageHandlerContext context)
    {
        var domainEvents = aggregateRoot.DomainEvents.ToList();
        foreach (var domainEvent in domainEvents)
        {
            await context.Publish(domainEvent);
        }
        aggregateRoot.ClearDomainEvents();
    }
}
