using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace ACME.BankingPlatform.API.Accounts.Domain.Model.Aggregates;

public partial class Account : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] 
    public DateTimeOffset? CreatedDate { get; set; }

    [Column("UpdatedAt")] 
    public DateTimeOffset? UpdatedDate { get; set; }
}