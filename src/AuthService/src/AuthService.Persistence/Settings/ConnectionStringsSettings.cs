using Cayd.AspNetCore.Settings;

namespace AuthService.Persistence.Settings
{
    public class ConnectionStringsSetting : ISettings
    {
        public static string SettingsKey => "ConnectionStrings";

        public required string AuthDb { get; set; }
    }
}