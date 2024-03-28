using Inventario.API.Models;
using Inventario.Domain.AggregatesRoot;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Infrastructure.ESEntityFramework;

namespace Inventario.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DbContextInventario Context;

    public UsuarioRepository(DbContextInventario context)
    {
        Context = context;
    }

    public void Add(Usuario usuario)
    {
        Context.Usuarios.Add(usuario);
    }

    public Usuario GetUsuarioById(Guid idUsuario)
    {
        return Context.Usuarios.FirstOrDefault(p => p.Id == idUsuario);
    }

    public Usuario ObtenerTokenUsuario(Login login)
    {
        return Context.Usuarios.FirstOrDefault(
            p => p.Nombre_Usuario == login.Usuario &&
                p.Contrasenya == login.Contrasenya);
    }
}
