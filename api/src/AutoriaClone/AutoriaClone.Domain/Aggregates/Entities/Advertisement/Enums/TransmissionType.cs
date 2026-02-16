namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;

public enum TransmissionType
{
    // Ручна / Механіка
    Manual = 1,

    // Автомат
    Automatic = 2,

    // Типтронік
    Tiptronic = 3,

    // Робот
    Robot = 4,

    // Варіатор (CVT)
    Variator = 5,

    // Редуктор (зазвичай для електрокарів)
    Reducer = 6
}

public static class TransmissionTypeExtensions
{
    public static string ToFriendlyString(this TransmissionType type) 
        => type switch
        {
            TransmissionType.Manual => "Ручна / Механіка",
            TransmissionType.Automatic => "Автомат",
            TransmissionType.Tiptronic => "Типтронік",
            TransmissionType.Robot => "Робот",
            TransmissionType.Variator => "Варіатор",
            TransmissionType.Reducer => "Редуктор",
            _ => "Unknown"
        };
}