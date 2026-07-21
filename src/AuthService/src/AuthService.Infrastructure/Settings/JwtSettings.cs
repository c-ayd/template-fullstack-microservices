using Cayd.AspNetCore.Settings;

namespace AuthService.Infrastructure.Settings
{
    public class JwtSettings : ISettings
    {
        public static string SettingsKey => "Jwt";

        public required string KeyId { get; set; }
        public required string PrivateKeyPath { get; set; }
        public required string PublicKeyPath { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int AccessTokenLifespanInMinutes { get; set; }
        public required int RefreshTokenLifespanInDays { get; set; }
    }
}
