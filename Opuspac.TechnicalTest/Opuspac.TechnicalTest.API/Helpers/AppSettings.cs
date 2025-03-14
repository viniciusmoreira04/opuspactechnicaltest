namespace Opuspac.TechnicalTest.Portal.Server.Helpers;

public class AppSettings
{
    public string JwtSecret { get; set; }
    public int AccessTokenDurationInMinutes { get; set; }
    public int RefreshTokenDurationInDays { get; set; }
    public string ExceptionWebhookUrl { get; set; }
}