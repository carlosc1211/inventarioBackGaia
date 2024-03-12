using System.Text;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Pagos.API.Controllers;
using Pagos.Fixtures;
using Pagos.Fixtures.Extensions;
using SharedKernel.BaseClasses;
using static Pagos.Service.Pagos.Commands.Commands;

namespace Pagos.API.Test.Controllers;

public class PagosControllerTests : IClassFixture<PagosInMemoryAppFactory<Startup>>
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly PagosInMemoryAppFactory<Startup> _appFactory;
    private readonly HttpClient _client;

    public PagosControllerTests(PagosInMemoryAppFactory<Startup> appFactory)
    {
        _mockMediator = new Mock<IMediator>();
        _appFactory = appFactory;
        _client = _appFactory.CreateClient();
    }

    #region COMMANDS
    [Theory]
    [LoadData("PagosCommands", "CrearPagoCommand")]
    public async Task CrearPagoCommand_Ok(CrearPagoCommand cmd)
    {
        var controller = new PagosController(_mockMediator.Object);
        var result = await controller.CrearPago(cmd);
        result.Should().BeOfType<OkResult>();
    }

    [Theory]
    [LoadData("PagosCommands", "CrearPagoCommand")]
    public async Task CrearPagoCommandAPI_Ok(CrearPagoCommand cmd)
    {
        var httpContent = new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(String.Format("api/pagos"), httpContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [LoadData("PagosCommands", "CrearPagoCommand")]
    public async Task ConciliarPagoExistenteCommand_Ok(CrearPagoCommand cmd)
    {
        var controller = new PagosController(_mockMediator.Object);
        await controller.CrearPago(cmd);
        
        var reconciliarPagoCommand = new ReconciliarPagoCommand();
        reconciliarPagoCommand.CodigoPedidoId = "112233-A";
        var result = await controller.ReconciliarPago(reconciliarPagoCommand);

        result.Should().BeOfType<OkResult>();
    }

    [Theory]
    [LoadData("PagosCommands", "ReconciliarPagoCommand")]
    public async Task ConciliarPagoExistenteCommandAPI_Ok(CrearPagoCommand cmd)
    {
        var httpContent = new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(String.Format("api/pagos/ReconciliarPago"), httpContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [LoadData("PagosCommands", "CrearPagoCommand")]
    public async Task ConciliarPagoNoExistenteCommand_Ok(CrearPagoCommand cmd)
    {
        var controller = new PagosController(_mockMediator.Object);
        
        var reconciliarPagoCommand = new ReconciliarPagoCommand();
        reconciliarPagoCommand.CodigoPedidoId = new CodigoPedidoId("112233-A");
        var result = await controller.ReconciliarPago(reconciliarPagoCommand);

        result.Should().BeOfType<OkResult>();
    }

    [Theory]
    [LoadData("PagosCommands", "CrearPagoCommand")]
    public async Task ConciliarPagoNoExistenteCommandAPI_Ok(CrearPagoCommand cmd)
    {
        var httpContent = new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(String.Format("api/pagos/ReconciliarPago"), httpContent);
        
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    #endregion

    #region QUERIES


    #endregion
}
