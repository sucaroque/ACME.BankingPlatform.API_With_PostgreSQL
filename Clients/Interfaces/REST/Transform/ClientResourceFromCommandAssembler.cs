using ACME.BankingPlatform.API.Clients.Application.Commands;
using ACME.BankingPlatform.API.Clients.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Clients.Interfaces.REST.Transform;

public static class ClientResourceFromCommandAssembler {
    public static ClientResource ToResourceFromRegisterClient(RegisterClient command) {
        return new ClientResource(command.Id, command.FirstName, command.LastName, command.Dni);
    }
}
