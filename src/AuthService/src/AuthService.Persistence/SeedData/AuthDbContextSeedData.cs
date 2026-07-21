using AuthService.Domain.Entities;
using AuthService.Persistence.DbContexts;
using AuthService.Persistence.Exceptions;
using AuthService.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Persistence.SeedData
{
    public static class AuthDbContextSeedData
    {
        public static async Task SeedDataAuthDbContextAsync(this IServiceProvider services, IConfiguration configuration)
        {
            using var scope = services.CreateAsyncScope();
            using var authDbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

            var seedDataSettings = configuration.GetSection(SeedDataSettings.SettingsKey).Get<SeedDataSettings>()!;

            await authDbContext.Database.MigrateAsync();

            await AddDefaultRolesAsync(authDbContext, seedDataSettings);
            await AddDefaultAccountsAsync(authDbContext, seedDataSettings);
        }

        private static async Task AddDefaultRolesAsync(AuthDbContext authDbContext, SeedDataSettings settings)
        {
            if (await authDbContext.Roles.AnyAsync())
                return;

            foreach (var role in settings.AuthDb.Roles)
            {
                await authDbContext.Roles.AddAsync(new Role(role));
            }

            await authDbContext.SaveChangesAsync();
        }

        private static async Task AddDefaultAccountsAsync(AuthDbContext authDbContext, SeedDataSettings settings)
        {
            if (await authDbContext.Accounts.AnyAsync())
                return;

            foreach (var accountRolePair in settings.AuthDb.Accounts)
            {
                var role = await authDbContext.Roles.FirstOrDefaultAsync(r => r.Name == accountRolePair.Role);
                if (role == null)
                    throw new SeedDataEntryNotFoundException($"A role named {accountRolePair.Role} is not found in the database.");

                var account = new Account(accountRolePair.Email, "123");

                account.IsEmailVerified = true;
                account.Roles.Add(role);
                await authDbContext.Accounts.AddAsync(account);
            }

            await authDbContext.SaveChangesAsync();
        }
    }
}
