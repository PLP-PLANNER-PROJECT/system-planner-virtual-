using Microsoft.AspNetCore.Mvc;
using Moq;
using Planner.Controllers;
using Planner.Models;
using Planner.Service;


namespace TestPlanner.Controller
{
    public class MetaControllerTests
    {
        private readonly Mock<MetaService> _metaServiceMock;
        private readonly MetaController _metaController;

        public MetaControllerTests()
        {
            _metaServiceMock = new Mock<MetaService>(null!);
            _metaController = new MetaController(_metaServiceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithMetas()
        {
            // Arrange
            var metas = new List<Meta>
        {
            new Meta { Id = 1, Titulo = "Meta 1" },
            new Meta { Id = 2, Titulo = "Meta 2" }
        };
            _metaServiceMock.Setup(service => service.GetAllMetasAsync()).ReturnsAsync(metas);

            // Act
            var result = await _metaController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Meta>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Detalhes_ReturnsViewResult_WithMeta_WhenMetaExists()
        {
            // Arrange
            var meta = new Meta { Id = 1, Titulo = "Meta 1" };
            _metaServiceMock.Setup(service => service.GetMetaByIdAsync(1)).ReturnsAsync(meta);

            // Act
            var result = await _metaController.Detalhes(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Meta>(viewResult.Model);
            Assert.Equal("Meta 1", model.Titulo);
        }

        [Fact]
        public async Task Detalhes_ReturnsNotFound_WhenMetaDoesNotExist()
        {
            // Arrange
            _metaServiceMock.Setup(service => service.GetMetaByIdAsync(1)).ReturnsAsync((Meta)null);

            // Act
            var result = await _metaController.Detalhes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Adicionar_ReturnsRedirectToIndex_AfterAddingMeta()
        {
            // Arrange
            var meta = new Meta { Id = 1, Titulo = "Meta Teste" };
            _metaServiceMock.Setup(service => service.AddMetaAsync(meta)).Returns(Task.CompletedTask);

            // Act
            var result = await _metaController.Adicionar(meta);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_metaController.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Editar_ReturnsRedirectToIndex_AfterUpdatingMeta()
        {
            // Arrange
            var meta = new Meta { Id = 1, Titulo = "Meta Teste" };
            _metaServiceMock.Setup(service => service.GetMetaByIdAsync(1)).ReturnsAsync(meta);
            _metaServiceMock.Setup(service => service.UpdateMetaAsync(meta)).Returns(Task.CompletedTask);

            // Act
            var result = await _metaController.Editar(1, meta);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_metaController.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Deletar_ReturnsViewResult_WithMeta_WhenMetaExists()
        {
            // Arrange
            var meta = new Meta { Id = 1, Titulo = "Meta 1" };
            _metaServiceMock.Setup(service => service.GetMetaByIdAsync(1)).ReturnsAsync(meta);

            // Act
            var result = await _metaController.Deletar(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Meta>(viewResult.Model);
            Assert.Equal("Meta 1", model.Titulo);
        }

        [Fact]
        public async Task DeletarConfirmado_ReturnsRedirectToIndex_AfterDeletingMeta()
        {
            // Arrange
            var meta = new Meta { Id = 1, Titulo = "Meta 1" };
            _metaServiceMock.Setup(service => service.GetMetaByIdAsync(1)).ReturnsAsync(meta);
            _metaServiceMock.Setup(service => service.DeleteMetaAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _metaController.DeletarConfirmado(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_metaController.Index), redirectToActionResult.ActionName);
        }
    }
}
