namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;

public enum PowerUnitType
{
    HorsePowers = 1,
    KiloWatts = 2
}

public static class PowerUnitExtensions
{
    public static string ToFriendlyString(this PowerUnitType unit) => unit switch
    {
        PowerUnitType.HorsePowers => "к/с",
        PowerUnitType.KiloWatts => "кВт",
        _ => "Unknown"
    };
}