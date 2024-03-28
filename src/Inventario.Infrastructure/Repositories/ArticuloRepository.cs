using Inventario.Domain.AggregatesRoot;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Infrastructure.ESEntityFramework;

namespace Inventario.Infrastructure.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly DbContextInventario Context;

        public ArticuloRepository(DbContextInventario context)
        {
            Context = context;
        }

        public void Add(Articulo articulo)
        {
            Context.Articulo.Add(articulo);
        }

        public Articulo GetArticuloById(Guid idArticulo)
        {
            return Context.Articulo.FirstOrDefault(p => p.Id == idArticulo);
        }

        public List<Articulo> ObtenerArticulos()
        {
            return Context.Articulo.OrderBy(p => p.Id).ToList();
        }
    }
}
