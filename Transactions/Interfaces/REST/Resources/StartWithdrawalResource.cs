using System.Text.Json.Serialization;

namespace ACME.BankingPlatform.API.Transactions.Interfaces.REST.Resources;

public record StartWithdrawalResource(long TransactionId, long AccountId, decimal Amount)
{
    [JsonIgnore]
    public long TransactionId { get; private set; } = TransactionId;

    public StartWithdrawalResource WithTransactionId(long transactionId)
    {
        return this with { TransactionId = transactionId };
    }
}
