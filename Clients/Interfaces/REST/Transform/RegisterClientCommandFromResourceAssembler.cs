using ACME.BankingPlatform.API.Clients.Application.Commands;
using ACME.BankingPlatform.API.Clients.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Clients.Interfaces.REST.Transform;

public static class RegisterClientCommandFromResourceAssembler {
    public static RegisterClient ToCommandFromResource(RegisterClientResource resource) {
        return new RegisterClient(resource.Id, resource.FirstName, resource.LastName, resource.Dni);
    }
}
