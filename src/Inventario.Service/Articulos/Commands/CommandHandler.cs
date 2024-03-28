using AutoMapper;
using Inventario.Domain.AggregatesRoot;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Infrastructure.ESEntityFramework;
using Inventario.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Transactions;
using static Inventario.Service.Articulos.Commands.Commands;

namespace Inventario.Service.Articulos.Commands
{
    public class CommandHandler : IRequestHandler<CrearArticuloCommand, Articulo>
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly IMapper _mapper;
        private readonly DbContextInventario _dbContextInventario;
        private readonly IConfiguration _configuration;
        public CommandHandler(IArticuloRepository articuloRepository,
                                IMapper mapper,
                                DbContextInventario dbContextInventario,
                                IConfiguration configuration)
        {
            _articuloRepository = articuloRepository;
            _mapper = mapper;
            _dbContextInventario = dbContextInventario;
            _configuration = configuration;
        }

        public async Task<Articulo> Handle(CrearArticuloCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var articuloExistente = _articuloRepository.GetArticuloById(request.Id);

                if (articuloExistente != null)
                    throw new ArgumentNullException($"El articulo {request.Id} ya ha sido creado");

                using (TransactionScope transaction = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.Serializable },
                TransactionScopeAsyncFlowOption.Enabled))
                {
                    var nuevoArticulo = Articulo.Crear(
                                    request.Model,
                                    request.SalesSample,
                                    request.SourcingTrackingNumber,
                                    request.Warrant,
                                    request.OriginCountry,
                                    request.DestinationCountry,
                                    request.Warehouse,
                                    request.TimeOfArrival,
                                    request.CurrentLocation,
                                    request.Incoterm,
                                    request.ContainerNumber);

                    _articuloRepository.Add(nuevoArticulo);
                    await _dbContextInventario.SaveChangesAsync();

                    transaction.Complete();

                    return nuevoArticulo;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Se ha producido un error al intentar crear el articulo: {ex.Message}", ex);
            }
        }
    }
}
