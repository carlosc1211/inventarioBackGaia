using System.Diagnostics.CodeAnalysis;

namespace Inventario.Domain.AggregatesRoot
{
    [ExcludeFromCodeCoverage]
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellidos { get; private set; }
        public string Nombre_Usuario { get; private set; }
        public string Contrasenya { get; private set; }

        protected Usuario() { }

        protected Usuario(
                string nombre,
                string apellidos,
                string nombre_usuario,
                string contrasenya)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Apellidos = apellidos;
            Nombre_Usuario = nombre_usuario;
            Contrasenya = contrasenya;
        }
        public static Usuario Crear(
            string nombre, string apellidos, string nombre_usuario, string contrasenya)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Apellidos = apellidos,
                Contrasenya = contrasenya,
                Nombre_Usuario = nombre_usuario
            };

            return nuevoUsuario;
        }
    }

}


