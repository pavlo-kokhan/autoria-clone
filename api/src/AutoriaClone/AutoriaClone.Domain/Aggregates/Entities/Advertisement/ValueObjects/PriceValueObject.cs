namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.ValueObjects;

public record PriceValueObject(int Value, string Currency, DateTime TimeSet)
{
    public static PriceValueObject Create(int value, string currency)
        => new(value, currency, DateTime.UtcNow);
}