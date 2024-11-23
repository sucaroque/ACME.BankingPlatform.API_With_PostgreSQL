using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Transform;

public static class StartDepositResourceFromCommandAssembler {
    public static StartDepositResource ToResourceFromDepositMoney(StartDeposit command) {
        return new StartDepositResource(command.TransactionId, command.AccountId, command.Amount);
    }
}
