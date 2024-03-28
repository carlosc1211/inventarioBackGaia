using Inventario.Domain.AggregatesRoot;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Domain.Interfaces.Services;
using Inventario.Infrastructure.ESEntityFramework;

namespace Inventario.Service.Articulos
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly DbContextInventario Context;

        public ArticuloService(IArticuloRepository articuloRepository, DbContextInventario context)
        {
            _articuloRepository = articuloRepository;
            Context = context;
        }

        public void Add(Articulo articulo)
        {
            Context.Articulo.Add(articulo);
        }

        public Articulo GetArticuloById(Guid idArticulo)
        {
            return _articuloRepository.GetArticuloById(idArticulo);
        }

    }
}
