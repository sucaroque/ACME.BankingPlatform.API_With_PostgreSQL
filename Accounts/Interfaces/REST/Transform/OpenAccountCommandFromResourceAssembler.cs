using ACME.BankingPlatform.API.Accounts.Application.Commands;
using ACME.BankingPlatform.API.Accounts.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Accounts.Interfaces.REST.Transform;

public static class OpenAccountCommandFromResourceAssembler {
    public static OpenAccount ToCommandFromResource(OpenAccountResource resource) {
        return new OpenAccount(resource.Id, resource.Number, resource.OverdraftLimit, resource.ClientId);
    }
}
