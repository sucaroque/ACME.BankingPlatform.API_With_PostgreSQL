using System.Text.Json.Serialization;

namespace ACME.BankingPlatform.API.Accounts.Interfaces.REST.Resources;

public record OpenAccountResource(long Id, string Number, decimal OverdraftLimit, long ClientId) 
{
    [JsonIgnore]
    public long Id { get; private set; } = Id;
    
    public OpenAccountResource WithId(long id) {
        return this with { Id = id };
    }
}
