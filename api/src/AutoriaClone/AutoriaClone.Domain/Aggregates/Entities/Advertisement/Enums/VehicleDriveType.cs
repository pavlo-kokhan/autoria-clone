namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;

public enum VehicleDriveType
{
    // Повний
    AllWheel = 1,

    // Передній
    Front = 2,

    // Задній
    Rear = 3
}

public static class DriveTypeExtensions
{
    public static string ToFriendlyString(this VehicleDriveType type) 
        => type switch
        {
            VehicleDriveType.AllWheel => "Повний",
            VehicleDriveType.Front => "Передній",
            VehicleDriveType.Rear => "Задній",
            _ => "Unknown"
        };
}