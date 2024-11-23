using ACME.BankingPlatform.API.Accounts.Application.Commands;
using ACME.BankingPlatform.API.Accounts.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Accounts.Interfaces.REST.Transform;

public static class AccountResourceFromCommandAssembler {
    public static AccountResource ToResourceFromOpenAccount(OpenAccount command) {
        return new AccountResource(command.Id, command.Number, command.OverdraftLimit, command.ClientId);
    }
}
