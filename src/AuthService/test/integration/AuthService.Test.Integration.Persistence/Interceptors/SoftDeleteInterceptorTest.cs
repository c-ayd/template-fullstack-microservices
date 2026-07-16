using AuthService.Domain.Entities;
using AuthService.Domain.SeedWork;
using AuthService.Test.Integration.Persistence.Collections;
using AuthService.Test.Utility.Fixtures;
using Cayd.Test.Generators;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Test.Integration.Persistence.Interceptors
{
    [Collection(nameof(AuthDbContextCollection))]
    public class SoftDeleteInterceptorTest
    {
        private readonly AuthDbContextFixture _authDbContextFixture;

        public SoftDeleteInterceptorTest(AuthDbContextFixture authDbContextFixture)
        {
            _authDbContextFixture = authDbContextFixture;
        }

        [Fact]
        public async Task SoftDeleteInterceptor_WhenEntityIsSoftDeleteableAndDeleted_ShouldNotDeleteEntityAndUpdateSoftDeleteProperties()
        {
            // Arrange
            using var authDbContext = _authDbContextFixture.CreateAuthDbContext();

            var now = DateTime.UtcNow;
            var account = new Account(EmailGenerator.Generate(), PasswordGenerator.Generate());
            var accountId = account.Id;

            await authDbContext.Accounts.AddAsync(account);
            await authDbContext.SaveChangesAsync();

            // Act
            authDbContext.Accounts.Remove(account);
            await authDbContext.SaveChangesAsync();

            // Assert
            authDbContext.ChangeTracker.Clear();
            var softDeletedAccount = await authDbContext.Accounts.FindAsync(accountId);

            Assert.NotNull(softDeletedAccount);
            Assert.True(softDeletedAccount.IsDeleted, $"The {nameof(ISoftDelete.IsDeleted)} property is not true.");
            Assert.NotNull(softDeletedAccount.DeletedDate);
            Assert.True(DateTime.Compare(now, softDeletedAccount.DeletedDate.Value) <= 0, $"The {nameof(ISoftDelete.DeletedDate)} property is in the past.");
        }

        [Fact]
        public async Task SoftDeleteInterceptor_WhenEntityIsNotSoftDeleteableAndDeleted_ShouldDeleteEntity()
        {
            // Arrange
            using var authDbContext = _authDbContextFixture.CreateAuthDbContext();

            var account = new Account(EmailGenerator.Generate(), PasswordGenerator.Generate());
            var login = new Login(account.Id, StringGenerator.GenerateUsingAsciiChars(10), DateTime.UtcNow);
            var loginId = login.Id;

            account.Logins.Add(login);
            await authDbContext.Accounts.AddAsync(account);
            await authDbContext.SaveChangesAsync();

            // Act
            authDbContext.Logins.Remove(login);
            await authDbContext.SaveChangesAsync();

            // Assert
            authDbContext.ChangeTracker.Clear();
            var deletedLogin = await authDbContext.Logins.FindAsync(loginId);

            Assert.Null(deletedLogin);
        }
    }
}