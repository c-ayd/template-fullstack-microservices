using Cayd.AspNetCore.Settings;

namespace AuthService.Persistence.Settings
{
    public class SeedDataSettings : ISettings
    {
        public static string SettingsKey => "SeedData";

        public required AuthDbData AuthDb { get; set; }

        public class AuthDbData
        {
            public required List<string> Roles { get; set; }
            public required List<AccountRolePair> Accounts { get; set; }

            public class AccountRolePair
            {
                public required string Email { get; set; }
                public required string Role { get; set; }
            }
        }
    }
}