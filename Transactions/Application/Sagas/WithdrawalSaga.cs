using ACME.BankingPlatform.API.Accounts.Application.Commands;
using ACME.BankingPlatform.API.Accounts.Domain.Events;
using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Application.OutboundServices.ACL;
using ACME.BankingPlatform.API.Transactions.Domain.Events;

namespace ACME.BankingPlatform.API.Transactions.Application.Sagas;

public class WithdrawalSaga(ExternalAccountsContextService externalAccountsContextService) :
    Saga<WithdrawalSagaData>,
    IAmStartedByMessages<WithdrawalStarted>,
    IHandleMessages<AccountNotFound>,
    IHandleMessages<InsufficientFundsDetected>,
    IHandleMessages<AccountDebited>
{
    public async Task Handle(WithdrawalStarted @event, IMessageHandlerContext context)
    {
        try
        {
            Data.TransactionId = @event.TransactionId;
            Data.AccountId = @event.AccountId;
            Data.Amount = @event.Amount;
            var existsAccount = await externalAccountsContextService.ExistsAccountById(Data.AccountId);
            if (existsAccount)
            {
                var command = new DebitAccount(Data.TransactionId, Data.AccountId, Data.Amount);
                await context.Send(command).ConfigureAwait(false);
            }
            else
            {
                var accountNotFound = new AccountNotFound(Data.TransactionId);
                await context.Publish(accountNotFound);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }
    
    public async Task Handle(AccountNotFound @event, IMessageHandlerContext context)
    {
        try
        {
            var command = new MarkWithdrawalAsFailed(Data.TransactionId);
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }
    
    public async Task Handle(InsufficientFundsDetected @event, IMessageHandlerContext context)
    {
        try
        {
            var command = new MarkWithdrawalAsFailed(Data.TransactionId);
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    public async Task Handle(AccountDebited @event, IMessageHandlerContext context)
    {
        try
        {
            var command = new MarkWithdrawalAsCompleted(Data.TransactionId);
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<WithdrawalSagaData> mapper)
    {
        mapper.MapSaga(saga => saga.TransactionId)
            .ToMessage<WithdrawalStarted>(message => message.TransactionId)
            .ToMessage<AccountNotFound>(message => message.TransactionId)
            .ToMessage<InsufficientFundsDetected>(message => message.TransactionId)
            .ToMessage<AccountDebited>(message => message.TransactionId);
    }
}

public class WithdrawalSagaData : ContainSagaData
{
    public long TransactionId { get; set; }
    public long AccountId { get; set; }
    public decimal Amount { get; set; }
}
