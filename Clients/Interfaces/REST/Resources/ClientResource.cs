using ACME.BankingPlatform.API.Shared.Interfaces.REST.Resources;
using System.Text.Json.Serialization;

namespace ACME.BankingPlatform.API.Clients.Interfaces.REST.Resources;

public record ClientResource(long Id, string FirstName, string LastName, string Dni)
{
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; private set; } = Id;
}
