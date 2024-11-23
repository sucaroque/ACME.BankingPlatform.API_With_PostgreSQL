namespace ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

public record Money(decimal Amount, ECurrency Currency = ECurrency.USD)
{
    public Money(): this(0, ECurrency.USD)
    {
    }

    public static Money Dollars(decimal amount)
    {
        return new Money(amount, ECurrency.USD);
    }

    public static Money Soles(decimal amount)
    {
        return new Money(amount, ECurrency.PEN);
    }

    public static Money Euros(decimal amount)
    {
        return new Money(amount, ECurrency.EUR);
    }

    public Money Add(Money money)
    {
        var total = Amount + money.Amount;
        return new Money(total, money.Currency);
    }

    public Money Subtract(Money money)
    {
        var total = Amount - money.Amount;
        return new Money(total, money.Currency);
    }
}
