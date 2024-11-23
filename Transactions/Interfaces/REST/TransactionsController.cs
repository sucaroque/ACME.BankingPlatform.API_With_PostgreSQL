using System.Net.Mime;
using ACME.BankingPlatform.API.Transactions.Application.Commands.Services;
using ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;
using ACME.BankingPlatform.API.Transactions.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using TSID.Creator.NET;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class TransactionsController
    (TransactionCommandService transactionCommandService) : ControllerBase
{
    [HttpPost("deposits")]
    public async Task<ActionResult<StartDepositResponseResource>> StartDeposit([FromBody] StartDepositResource resource)
    {
        try
        {
            var transactionId = TsidCreator.GetTsid().ToLong();
            resource = resource.WithTransactionId(transactionId);
            var command = StartDepositCommandFromResourceAssembler.ToCommandFromResource(resource);
            var notification = await transactionCommandService.Deposit(command);
            if (notification.HasErrors) {
                var errorResponse = new StartDepositResponseResource(null, notification.Errors.ToList());
                return BadRequest(errorResponse);
            }
            var depositMoneyResource = StartDepositResourceFromCommandAssembler.ToResourceFromDepositMoney(command);
            var successResponse = new StartDepositResponseResource(depositMoneyResource, null);
            return Accepted(successResponse);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new StartDepositResponseResource(null, null));
        }
    }
    
    [HttpPost("withdrawals")]
    public async Task<ActionResult<StartWithdrawalResponseResource>> StartWithdrawal([FromBody] StartWithdrawalResource resource)
    {
        try
        {
            var transactionId = TsidCreator.GetTsid().ToLong();
            resource = resource.WithTransactionId(transactionId);
            var command = StartWithdrawalCommandFromResourceAssembler.ToCommandFromResource(resource);
            var notification = await transactionCommandService.Withdraw(command);
            if (notification.HasErrors) {
                var errorResponse = new StartWithdrawalResponseResource(null, notification.Errors.ToList());
                return BadRequest(errorResponse);
            }
            var depositMoneyResource = StartWithdrawalResourceFromCommandAssembler.ToResourceFromDepositMoney(command);
            var successResponse = new StartWithdrawalResponseResource(depositMoneyResource, null);
            return Accepted(successResponse);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new StartWithdrawalResponseResource(null, null));
        }
    }
    
    [HttpPost("transfers")]
    public async Task<ActionResult<StartTransferResponseResource>> StartTransfer([FromBody] StartTransferResource resource)
    {
        try
        {
            var transactionId = TsidCreator.GetTsid().ToLong();
            resource = resource.WithTransactionId(transactionId);
            var command = StartTransferCommandFromResourceAssembler.ToCommandFromResource(resource);
            var notification = await transactionCommandService.Transfer(command);
            if (notification.HasErrors) {
                var errorResponse = new StartTransferResponseResource(null, notification.Errors.ToList());
                return BadRequest(errorResponse);
            }
            var depositMoneyResource = StartTransferResourceFromCommandAssembler.ToResourceFromDepositMoney(command);
            var successResponse = new StartTransferResponseResource(depositMoneyResource, null);
            return Accepted(successResponse);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new StartTransferResponseResource(null, null));
        }
    }
}
