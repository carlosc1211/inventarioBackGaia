using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Inventario.API.Extensions;
using Serilog;

namespace Inventario.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    IConfiguration configuration = config.Build();

                    var appConfigurationConnection = configuration["AppConfigurationConnection"];
                    var roleName = configuration["CloudRoleName"];
                    if (!string.IsNullOrEmpty(appConfigurationConnection) && !string.IsNullOrEmpty(roleName))
                    {
                        config.AddAzureAppConfigurationCustom(appConfigurationConnection, roleName);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}