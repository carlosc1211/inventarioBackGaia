using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Inventario.API.Extensions
{
    public static class AppConfigurationExtension
    {
        public static IConfigurationBuilder AddAzureAppConfigurationCustom(this IConfigurationBuilder configuration, string appConfigurationUri, string cloudRoleName)
        {
            var defaultCredentials = new DefaultAzureCredential();
            configuration.AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri(appConfigurationUri), defaultCredentials)
                    .ConfigureKeyVault(kvOptions =>
                        {
                            kvOptions.SetCredential(defaultCredentials);
                        })
                    .Select($"{cloudRoleName}:*")
                    .Select("Common:*")
                    .TrimKeyPrefix($"{cloudRoleName}:")
                    .TrimKeyPrefix("Common:");
            });

            return configuration;
        }
    }
}