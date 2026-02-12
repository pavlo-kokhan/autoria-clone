namespace AutoriaClone.Domain.Aggregates.Validation;

public static class MediaValidationConstants
{
    public const long MaxFileSize = 100 * 1024 * 1024; // 100 MB

    public static readonly IReadOnlyCollection<string> AllowedMediaTypes =
    [
        "image/jpeg",
        "image/png",
        "image/webp",
        "video/mp4",
        "video/mpeg",
        "video/quicktime"
    ];

    public static readonly string[] AllowedExtensions =
    [
        ".jpg",
        ".jpeg",
        ".png",
        ".webp",
        ".mp4",
        ".mov"
    ];
}