namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Flags;

[Flags]
public enum MultimediaOptions : long
{
    None = 0,
    Aux = 1 << 0,
    Usb = 1 << 1,
    Bluetooth = 1 << 2,
    AndroidAuto = 1 << 3,
    CarPlay = 1 << 4
}

public static class MultimediaOptionsExtensions
{
    public static string ToFriendlyString(this MultimediaOptions option) => option switch
    {
        MultimediaOptions.Aux => "AUX",
        MultimediaOptions.Usb => "USB",
        MultimediaOptions.Bluetooth => "Bluetooth",
        MultimediaOptions.AndroidAuto => "Android Auto",
        MultimediaOptions.CarPlay => "Apple CarPlay",
        _ => "Немає"
    };
}