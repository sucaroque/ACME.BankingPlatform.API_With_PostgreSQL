namespace ACME.BankingPlatform.API.Transactions.Domain.Model.ValueObjects;

public enum ETransactionState
{
    STARTED = 1,
    IN_PROGRESS = 2,
    FAILED = 3,
    COMPLETED = 4
}
