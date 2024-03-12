using Inventario.API.Models;
using Inventario.Domain.AggretatesRoot;

namespace Inventario.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioById(Guid idUsuario);
        void Add(Usuario usuario);
        Usuario ObtenerTokenUsuario(Login login);
    }
}