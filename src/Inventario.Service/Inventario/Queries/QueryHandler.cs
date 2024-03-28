using Inventario.Domain.AggregatesRoot;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Domain.Interfaces.Services;
using MediatR;
using static Inventario.Service.Inventario.Queries.QueryModels;

namespace Inventario.Service.Inventario.Queries;

public class QueryHandler  : IRequestHandler<GetUsuario, Usuario>
{
  private readonly IUsuarioRepository _InventarioRepository;
  private readonly IUsuarioService _InventarioService;

  public QueryHandler(IUsuarioRepository InventarioRepository,
    IUsuarioService InventarioService)
  {
    _InventarioRepository = InventarioRepository;
    _InventarioService = InventarioService;
  }

  public Task<Usuario> Handle(GetUsuario request, CancellationToken cancellationToken)
  {
    return null;
  }
}