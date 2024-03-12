using Inventario.Domain.AggretatesRoot;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Domain.Interfaces.Services;
using Inventario.Infrastructure.ESEntityFramework;

namespace Inventario.Service.Inventario;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _UsuarioRepository;
    private readonly DbContextInventario Context;

    public UsuarioService(IUsuarioRepository UsuarioRepository, DbContextInventario context)
    {
        _UsuarioRepository = UsuarioRepository;
        Context = context;
    }

    public void Add(Usuario usuario)
    {
        Context.Usuarios.Add(usuario);
    }

    public Usuario GetUsuarioById(Guid idUsuario)
    {
        return _UsuarioRepository.GetUsuarioById(idUsuario);
    }


}
