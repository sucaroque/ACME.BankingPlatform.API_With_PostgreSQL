using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Transactions.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.ComplexProperty(p => p.Amount,
            n =>
            {
                n.Property(p => p.Amount)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("amount");
                n.Property(a => a.Currency)
                    .HasConversion(new ECurrencyToStringConverter())
                    .HasMaxLength(3)
                    .HasColumnName("currency");
            });
        
        builder.Property(e => e.State)
            .HasColumnType("smallint")
            .HasConversion(
                v => (byte)v,
                v => (ETransactionState)v)
            .IsRequired();

        builder.HasDiscriminator(p => p.Type)
            .HasValue<Deposit>(ETransactionType.DEPOSIT)
            .HasValue<Withdrawal>(ETransactionType.WITHDRAWAL)
            .HasValue<Transfer>(ETransactionType.TRANSFER);
        
        builder.Property(p => p.Type)
            .HasColumnType("smallint")
            .HasConversion(
                v => (byte)v,
                v => (ETransactionType)v)
            .IsRequired();
        
        builder.Property(p => p.FromAccountId).IsRequired();
        
        /*builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(p => p.FromAccountId)
            .HasConstraintName("fk_transactions_from_account_id")
            .IsRequired();*/

        builder.HasIndex(p => p.FromAccountId)
            .HasDatabaseName("ix_transactions_from_account_id");
    }
}
