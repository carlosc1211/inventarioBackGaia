using Inventario.Domain.AggregatesRoot;

namespace Inventario.Domain.Interfaces.Repositories
{
    public interface IArticuloRepository
    {
        void Add(Articulo usuario);
        Articulo GetArticuloById(Guid idArticulo);
        List<Articulo> ObtenerArticulos();
    }
}
