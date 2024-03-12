using Inventario.API.Models;
using Inventario.Domain.AggretatesRoot;
using MediatR;

namespace Inventario.Service.Inventario.Commands;

public static class Commands
{
    public class CrearUsuarioCommand : IRequest<Usuario>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasenya { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Apellidos { get; set; }
    }

    public class LoginCommand : IRequest<string>
    {
        public string Usuario { get; set; }
        public string Contrasenya { get; set; }
    }
}
