using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Transform;

public static class StartWithdrawalResourceFromCommandAssembler {
    public static StartWithdrawalResource ToResourceFromDepositMoney(StartWithdrawal command) {
        return new StartWithdrawalResource(command.TransactionId, command.AccountId, command.Amount);
    }
}
