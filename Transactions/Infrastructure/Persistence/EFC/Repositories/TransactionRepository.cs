using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;

namespace ACME.BankingPlatform.API.Transactions.Infrastructure.Persistence.EFC.Repositories;

public class TransactionRepository<T>(AppDbContext context) : BaseRepository<T>(context), ITransactionRepository<T> where T : Transaction
{
}
