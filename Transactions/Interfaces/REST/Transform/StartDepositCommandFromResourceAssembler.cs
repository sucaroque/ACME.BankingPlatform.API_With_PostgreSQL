using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Transform;

public static class StartDepositCommandFromResourceAssembler {
    public static StartDeposit ToCommandFromResource(StartDepositResource resource) {
        return new StartDeposit(
            resource.TransactionId,
            resource.AccountId,
            resource.Amount);
    }
}
