using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Transform;

public static class StartWithdrawalCommandFromResourceAssembler {
    public static StartWithdrawal ToCommandFromResource(StartWithdrawalResource resource) {
        return new StartWithdrawal(
            resource.TransactionId,
            resource.AccountId,
            resource.Amount);
    }
}
