using System.Net.Mime;
using ACME.BankingPlatform.API.Accounts.Application.Commands.Services;
using ACME.BankingPlatform.API.Accounts.Interfaces.REST.Resources;
using ACME.BankingPlatform.API.Accounts.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using TSID.Creator.NET;

namespace ACME.BankingPlatform.API.Accounts.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AccountsController(AccountCommandService accountCommandService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OpenAccountResponseResource>> OpenAccount([FromBody] OpenAccountResource resource)
    {
        try
        {
            var accountId = TsidCreator.GetTsid().ToLong();
            resource = resource.WithId(accountId);
            var command = OpenAccountCommandFromResourceAssembler.ToCommandFromResource(resource);
            var notification = await accountCommandService.OpenAccount(command);
            if (notification.HasErrors)
            {
                var errorResponse = new OpenAccountResponseResource(null, notification.Errors.ToList());
                return BadRequest(errorResponse);
            }
            var accountResource = AccountResourceFromCommandAssembler.ToResourceFromOpenAccount(command);
            var successResponse = new OpenAccountResponseResource(accountResource, null);
            return Accepted(successResponse);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new OpenAccountResponseResource(null, null));
        }
    }
}
