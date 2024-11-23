using ACME.BankingPlatform.API.Accounts.Application.Commands;
using ACME.BankingPlatform.API.Accounts.Domain.Events;
using ACME.BankingPlatform.API.Transactions.Application.Commands;
using ACME.BankingPlatform.API.Transactions.Application.OutboundServices.ACL;
using ACME.BankingPlatform.API.Transactions.Domain.Events;

namespace ACME.BankingPlatform.API.Transactions.Application.Sagas;

public class TransferSaga(ExternalAccountsContextService externalAccountsContextService) :
    Saga<TransferSagaData>,
    IAmStartedByMessages<TransferStarted>,
    IHandleMessages<FromAccountNotFound>,
    IHandleMessages<InsufficientFundsDetected>,
    IHandleMessages<AccountDebited>,
    IHandleMessages<ToAccountNotFound>,
    IHandleMessages<FromAccountCredited>,
    IHandleMessages<AccountCredited>
{
    public async Task Handle(TransferStarted @event, IMessageHandlerContext context)
    {
        try
        {
            Data.TransactionId = @event.TransactionId;
            Data.FromAccountId = @event.FromAccountId;
            Data.ToAccountId = @event.ToAccountId;
            Data.Amount = @event.Amount;
            var existsFromAccount = await externalAccountsContextService.ExistsAccountById(Data.FromAccountId);
            if (existsFromAccount)
            {
                var command = new DebitAccount(Data.TransactionId, Data.FromAccountId, Data.Amount);
                await context.Send(command).ConfigureAwait(false);
            } 
            else 
            {
                var fromAccountNotFound = new FromAccountNotFound(Data.TransactionId);
                await context.Publish(fromAccountNotFound);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }
    
    public async Task Handle(FromAccountNotFound @event, IMessageHandlerContext context)
    {
        try
        {
            var command = new MarkTransferAsFailed(Data.TransactionId);
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
            var command = new MarkTransferAsFailed(Data.TransactionId);
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
            var existsToAccount = await externalAccountsContextService.ExistsAccountById(Data.ToAccountId);
            if (existsToAccount)
            {
                var deposit = new CreditAccount(Data.TransactionId, Data.ToAccountId, Data.Amount);
                await context.Send(deposit).ConfigureAwait(false);
            }
            else
            {
                var toAccountNotFound = new ToAccountNotFound(Data.TransactionId);
                await context.Publish(toAccountNotFound);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }
    
    public async Task Handle(ToAccountNotFound @event, IMessageHandlerContext context)
    {
        try
        {
            var command = new CreditFromAccount(Data.TransactionId, Data.FromAccountId, Data.Amount);
            await context.Send(command).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    public async Task Handle(FromAccountCredited @event, IMessageHandlerContext context)
    {
        try
        {
            var command = new MarkTransferAsFailed(Data.TransactionId);
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }
    
    public async Task Handle(AccountCredited @event, IMessageHandlerContext context)
    {
        try
        {
            var command = new MarkTransferAsCompleted(Data.TransactionId);
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransferSagaData> mapper)
    {
        mapper.MapSaga(saga => saga.TransactionId)
            .ToMessage<TransferStarted>(message => message.TransactionId)
            .ToMessage<FromAccountNotFound>(message => message.TransactionId)
            .ToMessage<InsufficientFundsDetected>(message => message.TransactionId)
            .ToMessage<AccountDebited>(message => message.TransactionId)
            .ToMessage<ToAccountNotFound>(message => message.TransactionId)
            .ToMessage<FromAccountCredited>(message => message.TransactionId)
            .ToMessage<AccountCredited>(message => message.TransactionId);
    }
}

public class TransferSagaData : ContainSagaData
{
    public long TransactionId { get; set; }
    public long FromAccountId { get; set; }
    public long ToAccountId { get; set; }
    public decimal Amount { get; set; }
}
