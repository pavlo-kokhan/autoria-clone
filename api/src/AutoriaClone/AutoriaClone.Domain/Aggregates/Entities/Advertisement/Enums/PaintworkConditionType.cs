namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;

public enum PaintworkConditionType
{
    // Як нове
    LikeNew = 1,

    // Професійно виправлені сліди використання
    ProfessionallyCorrected = 2,

    // Невиправлені сліди використання
    Uncorrected = 3
}

public static class PaintworkConditionExtensions
{
    public static string ToFriendlyString(this PaintworkConditionType condition) => condition switch
    {
        PaintworkConditionType.LikeNew => "Як нове",
        PaintworkConditionType.ProfessionallyCorrected => "Професійно виправлені сліди використання",
        PaintworkConditionType.Uncorrected => "Невиправлені сліди використання",
        _ => "Unknown"
    };

    public static string ToDescription(this PaintworkConditionType condition) => condition switch
    {
        PaintworkConditionType.LikeNew => "Оригінальне лакофарбове покриття, без слідів користування та підфарбовувань",
        PaintworkConditionType.ProfessionallyCorrected => "Наприклад, повторне лакування, дрібний ремонт, рихтування невеликих вм'ятин",
        PaintworkConditionType.Uncorrected => "Нормальне зношення, наприклад, невеликі вм'ятини, подряпини лакофарбового покриття, сколи",
        _ => string.Empty
    };
}