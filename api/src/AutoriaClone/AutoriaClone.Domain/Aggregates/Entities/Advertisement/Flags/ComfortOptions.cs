namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Flags;

[Flags]
public enum ComfortOptions : long
{
    None = 0,
    Conditioner = 1 << 0,        // 1
    ClimateControl = 1 << 1,     // 2
    CruiseControl = 1 << 2,      // 4
    HeatedSeats = 1 << 3,        // 8
    HeatedSteeringWheel = 1 << 4,// 16
    ElectricWindows = 1 << 5,    // 32
}

public static class ComfortOptionsExtensions
{
    public static string ToFriendlyString(this ComfortOptions option) => option switch
    {
        ComfortOptions.Conditioner => "Кондиціонер",
        ComfortOptions.ClimateControl => "Клімат-контроль",
        ComfortOptions.CruiseControl => "Круїз-контроль",
        ComfortOptions.HeatedSeats => "Підігрів сидінь",
        ComfortOptions.HeatedSteeringWheel => "Підігрів керма",
        ComfortOptions.ElectricWindows => "Електросклопідйомники",
        _ => "Немає"
    };
}