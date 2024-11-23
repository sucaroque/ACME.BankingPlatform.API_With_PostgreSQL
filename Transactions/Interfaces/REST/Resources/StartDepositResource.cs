using System.Text.Json.Serialization;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

public record StartDepositResource(long TransactionId, long AccountId, decimal Amount)
{
    [JsonIgnore]
    public long TransactionId { get; private set; } = TransactionId;

    public StartDepositResource WithTransactionId(long transactionId)
    {
        return this with { TransactionId = transactionId };
    }
}
