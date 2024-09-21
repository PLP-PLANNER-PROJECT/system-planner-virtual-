using Moq;
using Planner.Models;
using Planner.Repository;
using Planner.Service;


namespace TestPlanner.Service
{
    public class RelatorioServiceTest
    {
        private readonly RelatorioService _relatorioService;
        private readonly Mock<IRelatorioRepository> _relatorioRepositoryMock;

        public RelatorioServiceTest()
        {
            _relatorioRepositoryMock = new Mock<IRelatorioRepository>();
            _relatorioService = new RelatorioService(_relatorioRepositoryMock.Object);
        }

        [Fact]
        public async Task GerarRelatorioSemanalAsync_ReturnsRelatorio()
        {
            var inicioSemana = DateTime.Today;
            var relatorio = new Relatorio();

            _relatorioRepositoryMock.Setup(repo => repo.GerarRelatorioSemanalAsync(inicioSemana))
                .ReturnsAsync(relatorio);

            var result = await _relatorioService.GerarRelatorioSemanalAsync(inicioSemana);

            Assert.Equal(relatorio, result);
            _relatorioRepositoryMock.Verify(repo => repo.GerarRelatorioSemanalAsync(inicioSemana), Times.Once);
        }

        [Fact]
        public async Task GerarRelatorioMensalAsync_ReturnsRelatorio()
        {
            var inicioMes = new DateTime(2024, 9, 1);
            var relatorio = new Relatorio();

            _relatorioRepositoryMock.Setup(repo => repo.GerarRelatorioMensalAsync(inicioMes))
                .ReturnsAsync(relatorio);

            var result = await _relatorioService.GerarRelatorioMensalAsync(inicioMes);

            Assert.Equal(relatorio, result);
            _relatorioRepositoryMock.Verify(repo => repo.GerarRelatorioMensalAsync(inicioMes), Times.Once);
        }

        [Fact]
        public async Task GerarRelatorioAnualAsync_ReturnsRelatorio()
        {
            var inicioAno = new DateTime(2024, 1, 1);
            var relatorio = new Relatorio();

            _relatorioRepositoryMock.Setup(repo => repo.GerarRelatorioAnualAsync(inicioAno))
                .ReturnsAsync(relatorio);

            var result = await _relatorioService.GerarRelatorioAnualAsync(inicioAno);

            Assert.Equal(relatorio, result);
            _relatorioRepositoryMock.Verify(repo => repo.GerarRelatorioAnualAsync(inicioAno), Times.Once);
        }

        [Fact]
        public async Task GerarRelatorioPorPeriodoAsync_ReturnsRelatorio()
        {
            var dataInicio = new DateTime(2024, 9, 1);
            var dataFim = new DateTime(2024, 9, 30);
            var relatorio = new Relatorio();

            _relatorioRepositoryMock.Setup(repo => repo.GerarRelatorioPorPeriodoAsync(dataInicio, dataFim))
                .ReturnsAsync(relatorio);

            var result = await _relatorioService.GerarRelatorioPorPeriodoAsync(dataInicio, dataFim);

            Assert.Equal(relatorio, result);
            _relatorioRepositoryMock.Verify(repo => repo.GerarRelatorioPorPeriodoAsync(dataInicio, dataFim), Times.Once);
        }
    }
}
