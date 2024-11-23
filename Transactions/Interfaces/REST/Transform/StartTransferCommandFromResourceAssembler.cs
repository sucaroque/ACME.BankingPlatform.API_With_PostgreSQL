using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Transform;

public static class StartTransferCommandFromResourceAssembler {
    public static StartTransfer ToCommandFromResource(StartTransferResource resource) {
        return new StartTransfer(
            resource.TransactionId,
            resource.FromAccountId,
            resource.ToAccountId,
            resource.Amount);
    }
}
