namespace AF.Core.Settings;

public class JwtSettings
{
    public const string SectionName = nameof(JwtSettings);
    
    public string Issuer { get; init; } = string.Empty;
    
    public string Audience { get; init; } = string.Empty;
    
    public string Key { get; init; } = string.Empty;
}