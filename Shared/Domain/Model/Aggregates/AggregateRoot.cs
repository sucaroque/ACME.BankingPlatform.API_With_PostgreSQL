namespace ACME.BankingPlatform.API.Shared.Domain.Model.Aggregates;

public abstract class AggregateRoot
{
    private readonly List<IEvent> _domainEvents = [];

    public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
