using MediatR;
using Inventario.Domain.AggretatesRoot;

namespace Inventario.Service.Inventario.Queries;

public static class QueryModels
{
	public class GetUsuario : IRequest<Usuario>
	{
		public string IdUsuario { get; set; } = null!;
	}
}

