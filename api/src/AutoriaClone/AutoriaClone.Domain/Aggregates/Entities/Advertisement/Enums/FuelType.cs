namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;

public enum FuelType
{
    // Бензин
    Petrol = 1,

    // Газ (загальна категорія, або тільки газ)
    Gas = 2, 

    // Газ пропан-бутан / Бензин (LPG - Liquefied Petroleum Gas)
    PetrolLpg = 3, 

    // Газ метан / Бензин (CNG - Compressed Natural Gas)
    PetrolCng = 4, 

    // Гібрид (HEV - Hybrid Electric Vehicle)
    HybridHev = 5, 

    // Плагін-гібрид (PHEV - Plug-in Hybrid Electric Vehicle)
    HybridPhev = 6, 

    // М'який гібрид (MHEV - Mild Hybrid Electric Vehicle)
    HybridMhev = 7, 

    // Дизель
    Diesel = 8,

    // Електро
    Electric = 9
}

public static class FuelTypeExtensions
{
    public static string ToFriendlyString(this FuelType type) 
        => type switch
        {
            FuelType.Petrol => "Бензин",
            FuelType.Gas => "Газ",
            FuelType.PetrolLpg => "Газ пропан-бутан / Бензин",
            FuelType.PetrolCng => "Газ метан / Бензин",
            FuelType.HybridHev => "Гібрид (HEV)",
            FuelType.HybridPhev => "Гібрид (PHEV)",
            FuelType.HybridMhev => "Гібрид (MHEV)",
            FuelType.Diesel => "Дизель",
            FuelType.Electric => "Електро",
            _ => "Unknown"
        };
}