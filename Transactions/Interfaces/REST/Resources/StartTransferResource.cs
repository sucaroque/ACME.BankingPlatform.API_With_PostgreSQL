using System.Text.Json.Serialization;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

public record StartTransferResource(long TransactionId, long FromAccountId, long ToAccountId, decimal Amount)
{
    [JsonIgnore]
    public long TransactionId { get; private set; } = TransactionId;

    public StartTransferResource WithTransactionId(long transactionId)
    {
        return this with { TransactionId = transactionId };
    }
}
