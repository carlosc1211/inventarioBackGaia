using Inventario.Domain.AggretatesRoot;

namespace Inventario.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Usuario GetUsuarioById(Guid idUsuario);
        void Add(Usuario usuario);
    }
}