namespace AutoriaClone.Domain.Aggregates.ValueObjects.Address;

public record AddressValueObject(
    string Country,
    string City,
    string? Region,
    string? Street,
    string? BuildingNumber,
    int? Index);