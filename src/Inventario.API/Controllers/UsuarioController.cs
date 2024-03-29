using Inventario.API.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;
using static Inventario.Service.Inventario.Commands.Commands;

namespace Inventario.API.Controllers;

[ApiController]
[Route("api/Usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;

    private static readonly ILogger _logger = Log.ForContext<UsuarioController>();

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Login")]
    public Task<IActionResult> ObtenerUsuario([FromBody] LoginCommand request)
        => RequestHandler.HandleCommand(request, _mediator, _logger);

    [HttpPost]
    public Task<IActionResult> CrearUsuario([FromBody] CrearUsuarioCommand request)
        => RequestHandler.HandleCommand(request, _mediator, _logger);
}
