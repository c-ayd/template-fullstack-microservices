using AuthService.Persistence.DbContexts;
using AuthService.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connStrings = configuration.GetSection(ConnectionStringsSetting.SettingsKey).Get<ConnectionStringsSetting>()!;

            services.AddDbContext<AuthDbContext>(_ => _.UseNpgsql(connStrings.AuthDb));
        }
    }
}
