namespace AF.Core.Settings;

public class JwtConfiguration
{
    public const string SectionName = nameof(JwtConfiguration);
    
    public string Issuer { get; init; } = string.Empty;
    
    public string Audience { get; init; } = string.Empty;
    
    public string Key { get; init; } = string.Empty;
}