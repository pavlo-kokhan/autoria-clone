namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Flags;

[Flags]
public enum SafetyOptions : long
{
    None = 0,
    Abs = 1 << 0,
    Esp = 1 << 1,
    AirbagDriver = 1 << 2,
    CentralLock = 1 << 3
}

public static class SafetyOptionsExtensions
{
    public static string ToFriendlyString(this SafetyOptions option) => option switch
    {
        SafetyOptions.Abs => "ABS",
        SafetyOptions.Esp => "ESP",
        SafetyOptions.AirbagDriver => "Подушки безпеки",
        SafetyOptions.CentralLock => "Центральний замок",
        _ => "Немає"
    };
}