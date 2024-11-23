using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Validators;

public class StartWithdrawalValidator {
    
    public Notification Validate(StartWithdrawal command)
    {
        var notification = new Notification();

        var transactionId = command.TransactionId;
        if (transactionId <= 0) notification.AddError("transactionId must be greater than zero");

        var accountId = command.AccountId;
        if (accountId <= 0) notification.AddError("accountId must be greater than zero");

        var amount = command.Amount;
        if (amount <= 0) notification.AddError("amount must be greater than zero");

        return notification;
    }
}
