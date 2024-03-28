using Inventario.API.Shared;
using Inventario.Domain.AggregatesRoot;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Inventario.Service.Articulos.Commands.Commands;
using static Inventario.Service.Articulos.Queries.QueryModels;

namespace Inventario.API.Controllers;

[ApiController]
[Route("api/Articulo")]
public class ArticuloController : ControllerBase
{
    private readonly IMediator _mediator;

    private static readonly ILogger _logger = Log.ForContext<ArticuloController>();

    public ArticuloController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetAllItem")]
    public Task<IActionResult> ObtenerArticulos()
    => RequestHandler.HandleQuery(new ObtenerTodosArticulos {}, _mediator, _logger);

    [HttpPost]
    public Task<IActionResult> CrearUsuario([FromBody] CrearArticuloCommand request)
        => RequestHandler.HandleCommand(request, _mediator, _logger);
}
