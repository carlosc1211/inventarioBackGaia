using Inventario.Domain.AggregatesRoot;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Domain.Interfaces.Services;
using MediatR;
using static Inventario.Service.Articulos.Queries.QueryModels;

namespace Inventario.Service.Articulos.Queries
{
    public class QueryHandler : IRequestHandler<ObtenerTodosArticulos, List<Articulo>>
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly IArticuloService _articuloService;

        public QueryHandler(IArticuloRepository articuloRepository, IArticuloService articuloService)
        {
            _articuloRepository = articuloRepository;
            _articuloService = articuloService;
        }

        public async Task<List<Articulo>> Handle(ObtenerTodosArticulos request, CancellationToken cancellationToken)
        {
            var articulo = _articuloRepository.ObtenerArticulos().ToList();
            return articulo;
        }
    }
}
