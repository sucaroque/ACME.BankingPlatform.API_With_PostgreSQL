using ACME.BankingPlatform.API.Shared.Interfaces.REST.Resources;
using System.Text.Json.Serialization;

namespace ACME.BankingPlatform.API.Accounts.Interfaces.REST.Resources;

public record AccountResource(long Id, string Number, decimal OverdraftLimit, long ClientId)
{
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; private set; } = Id;
    
    [JsonConverter(typeof(LongToStringConverter))]
    public long ClientId { get; private set; } = ClientId;
}
