using Inventario.Domain.AggregatesRoot;

namespace Inventario.Domain.Interfaces.Services
{
    public interface IArticuloService
    {
        void Add(Articulo usuario);
        Articulo GetArticuloById(Guid idArticulo);
    }
}
