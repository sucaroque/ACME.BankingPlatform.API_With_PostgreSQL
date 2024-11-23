using ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Clients.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Clients.Interfaces.REST.Transform;

public static class ClientResourceFromEntityAssembler {
    public static ClientResource ToResourceFromEntity(Client entity) {
        return new ClientResource(entity.Id, entity.Name.FirstName, entity.Name.LastName, entity.Dni);
    }
}
