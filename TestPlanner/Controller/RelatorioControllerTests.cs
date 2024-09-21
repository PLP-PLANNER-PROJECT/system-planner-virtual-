using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlanner.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Planner.Controllers;
    using Planner.Models;
    using Planner.Service;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class RelatorioControllerTests
    {
        private readonly Mock<RelatorioService> _relatorioServiceMock;
        private readonly RelatorioController _controller;

        public RelatorioControllerTests()
        {
            _relatorioServiceMock = new Mock<RelatorioService>();
            _controller = new RelatorioController(_relatorioServiceMock.Object);
        }

        [Fact]
        public async Task RelatorioSemanal_ReturnsView_WithRelatorio()
        {
            // Arrange
            var inicioSemana = new DateTime(2024, 9, 1);
            var relatorio = new Relatorio { QuantidadeMetasCriadas = 5 };
            _relatorioServiceMock.Setup(service => service.GerarRelatorioSemanalAsync(inicioSemana)).ReturnsAsync(relatorio);

            // Act
            var result = await _controller.RelatorioSemanal(inicioSemana);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Relatorio>(viewResult.Model);
            Assert.Equal(5, model.QuantidadeMetasCriadas);
        }

        [Fact]
        public async Task RelatorioMensal_ReturnsView_WithRelatorio()
        {
            // Arrange
            var mes = new DateTime(2024, 1, 1);
            var relatorio = new Relatorio { QuantidadeMetasCriadas = 10 };
            _relatorioServiceMock.Setup(service => service.GerarRelatorioMensalAsync(mes)).ReturnsAsync(relatorio);

            // Act
            var result = await _controller.RelatorioMensal(mes);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Relatorio>(viewResult.Model);
            Assert.Equal(10, model.QuantidadeMetasCriadas);
        }

        [Fact]
        public async Task RelatorioAnual_ReturnsView_WithRelatorio()
        {
            // Arrange
            var ano = new DateTime(2024, 1, 1);
            var relatorio = new Relatorio { QuantidadeMetasCriadas = 15 };
            _relatorioServiceMock.Setup(service => service.GerarRelatorioAnualAsync(ano)).ReturnsAsync(relatorio);

            // Act
            var result = await _controller.RelatorioAnual(ano);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Relatorio>(viewResult.Model);
            Assert.Equal(15, model.QuantidadeMetasCriadas);
        }

        [Fact]
        public async Task RelatorioPorPeriodo_ReturnsView_WithRelatorio()
        {
            // Arrange
            var dataInicio = new DateTime(2024, 1, 1);
            var dataFim = new DateTime(2024, 1, 31);
            var relatorio = new Relatorio { QuantidadeMetasCriadas = 8 };
            _relatorioServiceMock.Setup(service => service.GerarRelatorioPorPeriodoAsync(dataInicio, dataFim)).ReturnsAsync(relatorio);

            // Act
            var result = await _controller.RelatorioPorPeriodo(dataInicio, dataFim);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Relatorio>(viewResult.Model);
            Assert.Equal(8, model.QuantidadeMetasCriadas);
        }
    }

}
