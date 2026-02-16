namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;

public enum EuroStandardType
{
    Euro1 = 1,
    Euro2 = 2,
    Euro3 = 3,
    Euro4 = 4,
    Euro5 = 5,
    Euro6 = 6
}

public static class EuroStandardExtensions
{
    public static string ToFriendlyString(this EuroStandardType standardType) => standardType switch
    {
        EuroStandardType.Euro1 => "Євро-1",
        EuroStandardType.Euro2 => "Євро-2",
        EuroStandardType.Euro3 => "Євро-3",
        EuroStandardType.Euro4 => "Євро-4",
        EuroStandardType.Euro5 => "Євро-5",
        EuroStandardType.Euro6 => "Євро-6",
        _ => "Unknown"
    };
}