using AutoMapper;
using Inventario.API.Models;
using Inventario.Domain.AggregatesRoot;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Domain.Models;
using Inventario.Infrastructure.ESEntityFramework;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using static Inventario.Service.Inventario.Commands.Commands;

namespace Inventario.Service.Inventario.Commands;

public class CommandHandler : IRequestHandler<CrearUsuarioCommand, Usuario>,
                                IRequestHandler<LoginCommand, Token>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly DbContextInventario _dbContextInventario;
    private readonly IConfiguration _configuration;

    public CommandHandler(IUsuarioRepository usuarioRepository,
                          IMapper mapper,
                          DbContextInventario dbContextInventario,
                          IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _dbContextInventario = dbContextInventario;
        _configuration = configuration;
    }

    public async Task<Usuario> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var usuarioExistente = _usuarioRepository.GetUsuarioById(request.Id);

            if (usuarioExistente != null)
                throw new ArgumentNullException($"El usuario {request.Nombre_Usuario} ya ha sido creado");

            using (TransactionScope transaction = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.Serializable },
                TransactionScopeAsyncFlowOption.Enabled))
            {
                var nuevoUsuario = Usuario.Crear(
                                request.Nombre, request.Apellidos, request.Nombre_Usuario, request.Contrasenya);

                _usuarioRepository.Add(nuevoUsuario);
                await _dbContextInventario.SaveChangesAsync();

                transaction.Complete();

                return nuevoUsuario;
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Se ha producido un error al intentar crear el pago: {ex.Message}", ex);
        }
    }

    public async Task<Token> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using (TransactionScope transaction = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.Serializable },
                TransactionScopeAsyncFlowOption.Enabled))
            {

                var login = new Login()
                {
                    Usuario = request.Usuario,
                    Contrasenya = request.Contrasenya
                };

                var usuario = _usuarioRepository.ObtenerTokenUsuario(login);

                if (usuario == null)
                    throw new ArgumentException($"No se encontró el usuario: {request.Usuario}");

                return ObtenerToken(usuario);
            }

        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"{ex.Message}", ex);
        }
    }

    private Token ObtenerToken(Usuario usuario)
    {
        //TODO -> VALIDAR CREDENCIALES CONTRA EL REPOSITORIO
        var clave = _configuration["Jwt:Key"];

        // Clave secreta para firmar el token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));

        // Crear las afirmaciones (claims) para el usuario autenticado
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Nombre_Usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        // Crear el token JWT
        var token = new JwtSecurityToken(
            issuer: "tu_issuer",
            audience: "tu_audience",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        // Serializar el token a una cadena
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        if (tokenString != null)
            return new Token()
            {
                token = tokenString,
            };

        return null;
    }
}
