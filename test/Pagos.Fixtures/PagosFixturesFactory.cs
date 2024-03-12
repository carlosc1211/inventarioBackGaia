using Microsoft.EntityFrameworkCore;
using Moq;
using Pagos.Domain.AggretatesRoot;
using Pagos.Domain.Interfaces.Repositories;
using Pagos.Domain.Interfaces.Services;
using Pagos.Infrastructure.ESEntityFramework;
using Pagos.Infrastructure.Repositories;
using SharedKernel.BaseClasses;

namespace Pagos.Fixtures;

public class PagosFixturesFactory
{
	public readonly TestDbContextPagos DbContextInstance;
	public readonly IPagosRepository PagosRepository;
	public Pago pago = Pago.Crear(
        "123456",
        new DateTime(2023,12,28,10,56,37),
        PasarelaPago.CaixaBank,
        "112233-A",
        "Visa",
        110,
        Moneda.Crear("Eur", "Euro", "€"),
        false,
        new DateTime(2023,12,28,10,57,57),
        true,
        new DateTime(2023,12,28,10,57,57)
    );


    public PagosFixturesFactory()
    {
        var contextOptions = new DbContextOptionsBuilder<DbContextPagos>()
             .UseInMemoryDatabase(Guid.NewGuid().ToString())
             .EnableSensitiveDataLogging()
             .Options;

        DbContextInstance = new TestDbContextPagos(contextOptions);
        PagosRepository = new PagosRepository(DbContextInstance);
    }
    private static void EnsureCreation(DbContextOptions<DbContextPagos> contextOptions)
    {
        using var context = new TestDbContextPagos(contextOptions);
        context.Database.EnsureCreated();
    }

    public Mock<IPagosService> MockPagosService()
    {
        Mock<IPagosService> _mockPagosService = new Mock<IPagosService>();
        _mockPagosService.Setup(s => s.ReconciliarPagos(It.IsAny<string>()));

        _mockPagosService.Setup(s => s.GuardarNuevoPago(It.IsAny<Pago>(), new CancellationToken()))
            .ReturnsAsync(pago);

        _mockPagosService.Setup(s => s.GetPagoByPedido(It.IsAny<string>()))
            .Returns(pago);

        return _mockPagosService;
    }

    public Mock<IPagosRepository> MockPagosRepository()
    {
        Mock<IPagosRepository> _mockPagosRepository = new Mock<IPagosRepository>();
        _mockPagosRepository.Setup(repo => repo.GetPagoByPedido(It.IsAny<string>()))
            .Returns(pago);
       
        return _mockPagosRepository;
    }
}