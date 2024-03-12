using Pagos.Domain.Interfaces.Repositories;
using Pagos.Domain.Interfaces.Services;
using Pagos.Infrastructure.ESEntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Testcontainers.RabbitMq;

namespace Pagos.Fixtures;

public class PagosInMemoryAppFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime where TStartup : class
{
    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder().Build();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        PagosFixturesFactory catalogoFixtures = new();

        var configurationBuilder = new ConfigurationBuilder();

        Environment.SetEnvironmentVariable("ConexionRabbitMQ", "amp://localhost:5672/");
        Environment.SetEnvironmentVariable("ConexionRepositorio", "Server=localhost,1433;Database=PAGOS;User=sa;Password=Dev@998877;Encrypt=False;");

        configurationBuilder.Sources.Clear();
        configurationBuilder.AddEnvironmentVariables();

        builder
        .UseEnvironment("Testing")
        .UseConfiguration(configurationBuilder.Build())
        .ConfigureTestServices(services =>
        {
            services.AddSingleton<DbContextPagos>(serviceProvider => catalogoFixtures.DbContextInstance);
            services.AddSingleton(services =>
       {
             ConnectionFactory factory = new()
             {
                 Uri = new Uri(_rabbitMqContainer.GetConnectionString()),
                 DispatchConsumersAsync = true,
                 AutomaticRecoveryEnabled = true
             };

             var connection = factory.CreateConnection();

             var channel = connection.CreateModel();

       return channel;
     });
     services.AddTransient<IPagosService>(sp => catalogoFixtures.MockPagosService().Object);
     services.AddTransient<IPagosRepository>(sp => catalogoFixtures.MockPagosRepository().Object);
      
    });

    }
    public Task InitializeAsync()
    {
        return _rabbitMqContainer.StartAsync();
    }
    public Task DisposeAsync()
    {
        return _rabbitMqContainer.DisposeAsync().AsTask();
    }
}

