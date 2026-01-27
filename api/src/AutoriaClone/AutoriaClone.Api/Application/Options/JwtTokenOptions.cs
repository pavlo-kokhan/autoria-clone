namespace AutoriaClone.Api.Application.Options;

public class JwtTokenOptions
{
     public const string SectionName = nameof(JwtTokenOptions);
     
     public required int ExpiresIn { get; set; }

     public required int RefreshTokenExpiresIn { get; set; }

     public required byte[] Key { get; set; }
}
