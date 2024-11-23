using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

public class ECurrencyToStringConverter() : ValueConverter<ECurrency, string>(v => v.ToString(),
    v => (ECurrency)Enum.Parse(typeof(ECurrency), v));
