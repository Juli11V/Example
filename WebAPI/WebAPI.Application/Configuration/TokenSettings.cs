namespace WebAPI.Infrastructure.Data;
public record TokenSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public int ExpirationMinutes { get; set; }
}

