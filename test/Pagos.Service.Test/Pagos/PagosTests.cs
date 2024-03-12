using AutoMapper;
using FluentAssertions;
using Moq;
using Pagos.Domain.AggretatesRoot;
using Pagos.Domain.Interfaces.Services;
using Pagos.Fixtures;
using Pagos.Fixtures.Extensions;
using Pagos.Service.Pagos.Commands;
using static Pagos.Service.Pagos.Commands.Commands;

namespace Pagos.Service.Test.Pagos
{

    public class PagosTests : IClassFixture<PagosFixturesFactory>
    {
        #region MocksServices
        private readonly Mock<IPagosService> _mockPagosService = new Mock<IPagosService>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        #endregion

        public PagosTests(PagosFixturesFactory _serviceFixtures)
        {
            _mockPagosService = _serviceFixtures.MockPagosService();
        }

        #region Commands
        [Theory]
        [LoadData("PagosCommands", "CrearPagoCommand")]
        public async Task CrearPagoCommand_CrearPago_Ok(CrearPagoCommand cmd)
        {
            var handler = new CommandHandler(_mockPagosService.Object, _mockMapper.Object);
            var result = await handler.Handle(cmd, new CancellationToken());

            result.Should().BeOfType<Pago>();
        }

        #endregion
    }
}