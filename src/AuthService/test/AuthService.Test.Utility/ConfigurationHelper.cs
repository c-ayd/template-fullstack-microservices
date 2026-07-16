using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace AuthService.Test.Utility
{
    public static class ConfigurationHelper
    {
        public static IConfiguration CreateConfiguration(params string[] filePaths)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
                .AddJsonFile("appsettings.Test.json")
                .Build();
        }
    }
}