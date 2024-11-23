using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;
using ACME.BankingPlatform.API.Transactions.Application.Commands.Validators;

namespace ACME.BankingPlatform.API.Transactions.Application.Commands.Services;

public class TransactionCommandService(IMessageSession messageSession, StartDepositValidator startDepositValidator, StartWithdrawalValidator startWithdrawalValidator, StartTransferValidator startTransferValidator)
{
    public async Task<Notification> Deposit(StartDeposit command)
    {
        var notification = startDepositValidator.Validate(command);
        if (notification.HasErrors) return notification;
        await messageSession.Send(command).ConfigureAwait(false);
        return notification;
    }
    
    public async Task<Notification> Withdraw(StartWithdrawal command)
    {
        var notification = startWithdrawalValidator.Validate(command);
        if (notification.HasErrors) return notification;
        await messageSession.Send(command).ConfigureAwait(false);
        return notification;
    }
    
    public async Task<Notification> Transfer(StartTransfer command)
    {
        var notification = startTransferValidator.Validate(command);
        if (notification.HasErrors) return notification;
        await messageSession.Send(command).ConfigureAwait(false);
        return notification;
    }
}
