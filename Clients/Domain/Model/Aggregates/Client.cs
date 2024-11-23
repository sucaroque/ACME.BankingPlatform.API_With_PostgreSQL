using ACME.BankingPlatform.API.Clients.Application.Commands;
using ACME.BankingPlatform.API.Clients.Domain.Events;
using ACME.BankingPlatform.API.Clients.Domain.Model.ValueObjects;
using ACME.BankingPlatform.API.Shared.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;

public partial class Client(long id, PersonName name, string dni, EClientStatus status) : AggregateRoot
{
    public long Id { get; private set; } = id;
    public PersonName Name { get; private set; } = name;
    public string Dni { get; private set; } = dni;
    public EClientStatus Status { get; private set; } = status;

    public Client() : this(0, new PersonName(), "", EClientStatus.INACTIVE)
    {
    }

    public void Register(RegisterClient command)
    {
        Id = command.Id;
        Name = new PersonName(command.FirstName, command.LastName);
        Dni = command.Dni;
        Status = EClientStatus.ACTIVE;
        var domainEvent = new ClientRegistered(command.Id, command.FirstName, command.LastName, command.Dni);
        AddDomainEvent(domainEvent);
    }
}
