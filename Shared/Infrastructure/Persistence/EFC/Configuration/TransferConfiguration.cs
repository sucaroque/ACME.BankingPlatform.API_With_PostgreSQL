using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.Property(p => p.ToAccountId).IsRequired();

        builder.Property(p => p.ToAccountId)
            .IsRequired();

        /*builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(p => p.ToAccountId)
            .HasConstraintName("fk_transactions_to_account_id")
            .IsRequired();*/

        builder.HasIndex(p => p.ToAccountId)
            .HasDatabaseName("ix_transactions_to_account_id");
    }
}
