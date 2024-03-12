using FluentAssertions;
using Pagos.Domain.AggretatesRoot;
using Pagos.Fixtures;
using Pagos.Infrastructure.Repositories;
using SharedKernel.BaseClasses;

namespace Pagos.Infrastructure.Test.Respositories;

public class PagosRepositoryTests : IClassFixture<PagosFixturesFactory>
{
    private readonly PagosRepository _repoPagos;
    private readonly TestDbContextPagos _contextPagos;

    public Pago pago = Pago.Crear(
        "123456",
        new DateTime(2023,12,28,10,56,37),
        PasarelaPago.CaixaBank,
        new CodigoPedidoId("112233-A"),
        "Visa",
        110,
        Moneda.Crear("Eur", "Euro", "€"),
        false,
        new DateTime(2023,12,28,10,57,57),
        true,
        new DateTime(2023,12,28,10,57,57)
    );

    public PagosRepositoryTests(PagosFixturesFactory factory)
    {
        _contextPagos = factory.DbContextInstance;
        _repoPagos = new PagosRepository(_contextPagos);
    }

    [Fact]
    public async Task Agregar_ShouldAddNewPago()
    {
        _repoPagos.AgregarPago(pago);
        await _repoPagos.UnitOfWork.SaveChangesAsync();

        _contextPagos.Pagos.FirstOrDefault(c => c.IdTransaccion == pago.IdTransaccion).Should().NotBeNull();
    }

    [Fact]
    public async Task GetByPedido_ShouldReturnCorrectPago()
    {
        var pago = new PagosFixturesFactory().pago;
        _repoPagos.AgregarPago(pago);
        await _repoPagos.UnitOfWork.SaveChangesAsync();

        var found = _repoPagos.GetPagoByPedido(pago.CodigoPedidoId);
        found.Should().NotBeNull();
        found.CodigoPedidoId.Should().Be(pago.CodigoPedidoId);
    }

    [Fact]
    public void GetByPedido_ShouldReturn_Null_IfNotFound()
    {
        var pago = new PagosFixturesFactory().pago;
        var actualPago = _repoPagos.GetPagoByPedido("112233-B");
        actualPago.Should().BeNull();
    }

    [Fact]
    public async Task GetByTransaccion_ShouldReturnCorrectPago()
    {
        var pago = new PagosFixturesFactory().pago;
        _repoPagos.AgregarPago(pago);
        await _repoPagos.UnitOfWork.SaveChangesAsync();

        var found = _repoPagos.GetPagoByTransaccionId(pago.IdTransaccion);
        found.Should().NotBeNull();
        found.IdTransaccion.Should().Be(pago.IdTransaccion);
    }

    [Fact]
    public void GetByTransaccion_ShouldReturn_Null_IfNotFound()
    {
        var pago = new PagosFixturesFactory().pago;
        var actualPago = _repoPagos.GetPagoByTransaccionId("111111");
        actualPago.Should().BeNull();
    }
}