using System.Text.Json.Serialization;

namespace ACME.BankingPlatform.API.Clients.Interfaces.REST.Resources;

public record RegisterClientResource(long Id, string FirstName, string LastName, string Dni) 
{
    [JsonIgnore]
    public long Id { get; private set; } = Id;
    
    public RegisterClientResource WithId(long id) {
        return this with { Id = id };
    }
}
