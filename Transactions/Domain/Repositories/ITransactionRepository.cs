using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;

namespace ACME.BankingPlatform.API.Transactions.Domain.Repositories;

public interface ITransactionRepository<T> : IBaseRepository<T> where T : Transaction
{
}
