using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Transform;

public static class StartTransferResourceFromCommandAssembler {
    public static StartTransferResource ToResourceFromDepositMoney(StartTransfer command) {
        return new StartTransferResource(command.TransactionId, command.FromAccountId, command.ToAccountId, command.Amount);
    }
}
