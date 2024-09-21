using Microsoft.AspNetCore.Mvc;
using Moq;
using Planner.Controllers;
using Planner.Models;
using Planner.Service;


namespace TestPlanner
{
    public class TarefaControllerTests
    {
        private readonly Mock<TarefaService> _tarefaServiceMock;
        private readonly TarefaController _tarefaController;

        public TarefaControllerTests()
        {
            _tarefaServiceMock = new Mock<TarefaService>(null!); // Substituir null pela dependência real se necessário
            _tarefaController = new TarefaController(_tarefaServiceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithTarefasForToday()
        {
            // Arrange
            var tarefas = new List<Tarefa>
            {
                new Tarefa { Id = 1, Titulo = "Tarefa 1", Dia = DateTime.Now },
                new Tarefa { Id = 2, Titulo = "Tarefa 2", Dia = DateTime.Now }
            };

            _tarefaServiceMock.Setup(service => service.GetAllTarefasAsync())
                .ReturnsAsync(tarefas);

            // Act
            var result = await _tarefaController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Tarefa>>(viewResult.Model);
            Assert.Equal(2, model?.Count());
        }

        [Fact]
        public async Task Detalhes_ReturnsNotFound_WhenTarefaDoesNotExist()
        {
            // Arrange
            _tarefaServiceMock.Setup(service => service.GetTarefaByIdAsync(1))
                .ReturnsAsync((Tarefa)null);

            // Act
            var result = await _tarefaController.Detalhes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Detalhes_ReturnsViewResult_WithTarefa()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa Teste", Dia = DateTime.Now };
            _tarefaServiceMock.Setup(service => service.GetTarefaByIdAsync(1))
                .ReturnsAsync(tarefa);

            // Act
            var result = await _tarefaController.Detalhes(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Tarefa>(viewResult.Model);
            Assert.Equal(tarefa, model);
        }

        [Fact]
        public async Task AdicionarPost_ReturnsRedirectToIndex_WhenModelIsValid()
        {
            // Arrange
            var novaTarefa = new Tarefa { Titulo = "Nova Tarefa" };

            // Act
            var result = await _tarefaController.Adicionar(novaTarefa);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_tarefaController.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task EditarPost_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa Atualizada" };

            // Act
            var result = await _tarefaController.Editar(2, tarefa);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeletarConfirmado_ReturnsRedirectToIndex_AfterDeletion()
        {
            // Arrange
            _tarefaServiceMock.Setup(service => service.DeleteTarefaAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _tarefaController.DeletarConfirmado(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_tarefaController.Index), redirectToActionResult.ActionName);
        }
    }
}
