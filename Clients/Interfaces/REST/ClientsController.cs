using System.Net.Mime;
using ACME.BankingPlatform.API.Clients.Interfaces.REST.Resources;
using ACME.BankingPlatform.API.Clients.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ACME.BankingPlatform.API.Clients.Application.Commands.Services;
using ACME.BankingPlatform.API.Clients.Application.Queries.Services;
using TSID.Creator.NET;

namespace ACME.BankingPlatform.API.Clients.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ClientsController(ClientCommandService clientCommandService, ClientQueryService clientQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<RegisterClientResponseResource>> RegisterClient([FromBody] RegisterClientResource resource)
    {
        try
        {
            var clientId = TsidCreator.GetTsid().ToLong();
            resource = resource.WithId(clientId);
            var command = RegisterClientCommandFromResourceAssembler.ToCommandFromResource(resource);
            var notification = await clientCommandService.Register(command);
            if (notification.HasErrors)
            {
                var errorResponse = new RegisterClientResponseResource(null, notification.Errors.ToList());
                return BadRequest(errorResponse);
            }
            var clientResource = ClientResourceFromCommandAssembler.ToResourceFromRegisterClient(command);
            var successResponse = new RegisterClientResponseResource(clientResource, null);
            return Accepted(successResponse);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new RegisterClientResponseResource(null, null));
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<GetClientsResponseResource>> GetClients(int page = 1, int limit = 10)
    {
        try
        {
            var (clients, paginationMetadata) = await clientQueryService.GetClients(page, limit);
            var clientResources = clients.Select(ClientResourceFromEntityAssembler.ToResourceFromEntity);
            var successResponse = new GetClientsResponseResource(clientResources, null);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(successResponse);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new GetClientsResponseResource(null, null));
        }
    }
}
