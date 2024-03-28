using Inventario.Domain.AggregatesRoot;
using MediatR;

namespace Inventario.Service.Articulos.Commands
{
    public static class Commands
    {
        public class CrearArticuloCommand : IRequest<Articulo>
        {
            public Guid Id { get; set; }
            public string Model { get; set; }
            public string SalesSample { get; set; }
            public string SourcingTrackingNumber { get; set; }
            public string Warrant { get; set; }
            public string OriginCountry { get; set; }
            public string DestinationCountry { get; set; }
            public string Warehouse { get; set; }
            public DateTime TimeOfArrival { get; set; }
            public string CurrentLocation { get; set; }
            public string Incoterm { get; set; }
            public string ContainerNumber { get; set; }
        }
    }
}
