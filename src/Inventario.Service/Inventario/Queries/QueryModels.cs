using MediatR;
using Inventario.Domain.AggregatesRoot;

namespace Inventario.Service.Inventario.Queries;

public static class QueryModels
{
	public class GetUsuario : IRequest<Usuario>
	{
		public string IdUsuario { get; set; } = null!;
	}
}

