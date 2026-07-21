using AuthService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace AuthService.Test.Utility.Fixtures
{
    public class AuthDbContextFixture : IAsyncLifetime
    {
        private PostgreSqlContainer _container = null!;

        public async Task InitializeAsync()
        {
            _container = new PostgreSqlBuilder(TestContainersImageVersions.PostgreSql)
                .Build();
            await _container.StartAsync();

            using var dbContext = CreateAuthDbContext();
            await dbContext.Database.MigrateAsync();
        }

        public AuthDbContext CreateAuthDbContext()
        {
            return new AuthDbContext(new DbContextOptionsBuilder<AuthDbContext>()
                .UseNpgsql(_container.GetConnectionString())
                .Options);
        }

        public async Task DisposeAsync()
        {
            await _container.StopAsync();
            await _container.DisposeAsync();
        }
    }
}
