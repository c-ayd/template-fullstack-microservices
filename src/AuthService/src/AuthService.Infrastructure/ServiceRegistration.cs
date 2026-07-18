using AuthService.Application.Abstractions.Authentication;
using AuthService.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IJwtKeyService, JwtKeyService>();
            services.AddSingleton<IJwtService, JwtService>();
        }
    }
}