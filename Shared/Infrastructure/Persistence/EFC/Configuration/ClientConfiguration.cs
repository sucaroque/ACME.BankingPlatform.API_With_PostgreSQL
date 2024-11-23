using ACME.BankingPlatform.API.Clients.Domain.Model.Aggregates;
using ACME.BankingPlatform.API.Clients.Domain.Model.ValueObjects;
using ACME.BankingPlatform.API.Transactions.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(c => c.Id)
            .HasColumnType("bigint")
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.OwnsOne(p => p.Name,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(75);
                n.Property(p => p.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(75);
            });

        builder.Property(c => c.Dni)
            .IsRequired()
            .HasMaxLength(8);
        
        builder.HasIndex(c => c.Dni)
            .IsUnique()
            .HasDatabaseName("uq_clients_dni");
        
        builder.Property(e => e.Status)
            .HasColumnType("smallint")
            .HasConversion(
                v => (byte)v,
                v => (EClientStatus)v)
            .IsRequired();
    }
}
