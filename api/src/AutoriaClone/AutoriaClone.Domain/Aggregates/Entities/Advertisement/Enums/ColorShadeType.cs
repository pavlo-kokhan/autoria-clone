namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;

public enum ColorShadeType
{
    Beige = 1,      // Бежевий
    Black = 2,      // Чорний
    Blue = 3,       // Синій
    Brown = 4,      // Коричневий
    Green = 5,      // Зелений
    Grey = 6,       // Сірий
    Orange = 7,     // Помаранчевий
    Purple = 8,     // Фіолетовий
    Red = 9,        // Червоний
    White = 10,     // Білий
    Yellow = 11     // Жовтий
}

public static class ColorShadeExtensions
{
    public static string ToFriendlyString(this ColorShadeType shadeType) => shadeType switch
    {
        ColorShadeType.Beige => "Бежевий",
        ColorShadeType.Black => "Чорний",
        ColorShadeType.Blue => "Синій",
        ColorShadeType.Brown => "Коричневий",
        ColorShadeType.Green => "Зелений",
        ColorShadeType.Grey => "Сірий",
        ColorShadeType.Orange => "Помаранчевий",
        ColorShadeType.Purple => "Фіолетовий",
        ColorShadeType.Red => "Червоний",
        ColorShadeType.White => "Білий",
        ColorShadeType.Yellow => "Жовтий",
        _ => "Unknown"
    };

    public static string ToHexCode(this ColorShadeType shadeType) => shadeType switch
    {
        ColorShadeType.Beige => "#F5F5DC",
        ColorShadeType.Black => "#000000",
        ColorShadeType.Blue => "#0000FF",
        ColorShadeType.Brown => "#A52A2A",
        ColorShadeType.Green => "#008000",
        ColorShadeType.Grey => "#808080",
        ColorShadeType.Orange => "#FFA500",
        ColorShadeType.Purple => "#800080",
        ColorShadeType.Red => "#FF0000",
        ColorShadeType.White => "#FFFFFF",
        ColorShadeType.Yellow => "#FFFF00",
        _ => "#FFFFFF"
    };
}