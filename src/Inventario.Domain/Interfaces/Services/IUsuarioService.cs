using Inventario.Domain.AggregatesRoot;

namespace Inventario.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Usuario GetUsuarioById(Guid idUsuario);
        void Add(Usuario usuario);
    }
}