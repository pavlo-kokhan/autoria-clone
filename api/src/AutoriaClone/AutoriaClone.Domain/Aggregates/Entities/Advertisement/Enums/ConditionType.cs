namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;

public enum ConditionType
{
    // Повністю непошкоджене
    Undamaged = 1,

    // Професійно відремонтовані пошкодження
    ProfessionallyRepaired = 2,

    // Невідремонтовані пошкодження
    UnrepairedDamages = 3,

    // Не на ходу / На запчастини
    NotRunningOrForParts = 4
}

public static class VehicleConditionExtensions
{
    public static string ToFriendlyString(this ConditionType condition) => condition switch
    {
        ConditionType.Undamaged => "Повністю непошкоджене",
        ConditionType.ProfessionallyRepaired => "Професійно відремонтовані пошкодження",
        ConditionType.UnrepairedDamages => "Невідремонтовані пошкодження",
        ConditionType.NotRunningOrForParts => "Не на ходу / На запчастини",
        _ => "Unknown"
    };

    public static string ToDescription(this ConditionType condition) => condition switch
    {
        ConditionType.Undamaged => "Пошкодження відсутні",
        ConditionType.ProfessionallyRepaired => "Пошкодження усунуті, не потребує ремонту",
        ConditionType.UnrepairedDamages => "Внаслідок бойових дій чи ДТП, пошкодження кузова, несправність рульового управління, коробки передач, осей, сліди граду тощо",
        ConditionType.NotRunningOrForParts => "Внаслідок бойових дій, ДТП чи пожежі, несправності двигуна тощо",
        _ => string.Empty
    };
}