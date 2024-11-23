using ACME.BankingPlatform.API.Accounts.Application.Commands;
using ACME.BankingPlatform.API.Accounts.Domain.Events;
using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Application.OutboundServices.ACL;
using ACME.BankingPlatform.API.Transactions.Domain.Events;

namespace ACME.BankingPlatform.API.Transactions.Application.Sagas;

public class DepositSaga(ExternalAccountsContextService externalAccountsContextService) :
    Saga<DepositSagaData>,
    IAmStartedByMessages<DepositStarted>,
    IHandleMessages<AccountNotFound>,
    IHandleMessages<AccountInvalidDataFound>,
    IHandleMessages<AccountCredited>
{
    public async Task Handle(DepositStarted @event, IMessageHandlerContext context)
    {
        try
        {
            Data.TransactionId = @event.TransactionId;
            Data.AccountId = @event.AccountId;
            Data.Amount = @event.Amount;
            var existsAccount = await externalAccountsContextService.ExistsAccountById(Data.AccountId);
            if (existsAccount)
            {
                var command = new CreditAccount(Data.TransactionId, Data.AccountId, Data.Amount);
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
            var command = new MarkDepositAsFailed(Data.TransactionId);
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }
    
    public async Task Handle(AccountInvalidDataFound @event, IMessageHandlerContext context)
    {
        try
        {
            var command = new MarkDepositAsFailed(Data.TransactionId);
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }
    
    public async Task Handle(AccountCredited accountCredited, IMessageHandlerContext context)
    {
        try
        {
            var command = new MarkDepositAsCompleted(Data.TransactionId);
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<DepositSagaData> mapper)
    {
        mapper.MapSaga(saga => saga.TransactionId)
            .ToMessage<DepositStarted>(message => message.TransactionId)
            .ToMessage<AccountNotFound>(message => message.TransactionId)
            .ToMessage<AccountInvalidDataFound>(message => message.TransactionId)
            .ToMessage<AccountCredited>(message => message.TransactionId);
    }
}

public class DepositSagaData : ContainSagaData
{
    public long TransactionId { get; set; }
    public long AccountId { get; set; }
    public decimal Amount { get; set; }
}
