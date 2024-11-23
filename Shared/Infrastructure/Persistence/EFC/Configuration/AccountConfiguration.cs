using ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Client = ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates.Client;

namespace ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasColumnType("bigint")
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Number)
            .IsRequired()
            .HasMaxLength(25);
        
        builder.HasIndex(c => c.Number)
            .IsUnique()
            .HasDatabaseName("uq_accounts_number");
        
        builder.ComplexProperty(p => p.Balance,
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
        
        builder.Property(p => p.ClientId).IsRequired();
        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(p => p.ClientId)
            .HasConstraintName("fk_accounts_client_id");
        builder.HasIndex(p => p.ClientId)
            .HasDatabaseName("ix_accounts_client_id");
    }
}
