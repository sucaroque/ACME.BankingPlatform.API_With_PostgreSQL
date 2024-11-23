using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Validators;

public class StartTransferValidator {
    
    public Notification Validate(StartTransfer command)
    {
        var notification = new Notification();

        var transactionId = command.TransactionId;
        if (transactionId <= 0) notification.AddError("transactionId must be greater than zero");

        var fromAccountId = command.FromAccountId;
        if (fromAccountId <= 0) notification.AddError("fromAccountId must be greater than zero");
        
        var toAccountId = command.ToAccountId;
        if (toAccountId <= 0) notification.AddError("toAccountId must be greater than zero");

        var amount = command.Amount;
        if (amount <= 0) notification.AddError("amount must be greater than zero");

        return notification;
    }
}
