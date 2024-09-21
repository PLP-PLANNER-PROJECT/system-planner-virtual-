using Planner.Service;
using Planner.IRepository;
using Moq;
using Planner.Models;
using Planner.Models.Enum;
using System.Runtime.CompilerServices;


namespace TestPlanner.Service
{
    //Testes comentados em MetaServiceTest.cs
    public class TarefaServiceTest
    {
        private readonly TarefaService _tarefaService;
        private readonly Mock<ITarefaRepository> _tarefaRepositoryMock;


        public TarefaServiceTest()
        {
            _tarefaRepositoryMock = new Mock<ITarefaRepository>();
            _tarefaService = new TarefaService(_tarefaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetTarefaByIdAsync_ReturnsMeta()
        {
            var tarefaId = 1;
            var tarefa = new Tarefa { Id = tarefaId };

            _tarefaRepositoryMock.Setup(repo => repo.GetByIdAsync(tarefaId))
                .ReturnsAsync(tarefa);

            var result = await _tarefaService.GetTarefaByIdAsync(tarefaId);

            Assert.Equal(tarefa, result);

            _tarefaRepositoryMock.Verify(repo => repo.GetByIdAsync(tarefaId), Times.Once);
        }

        [Fact]
        public async Task GetAllTarefasAsync_ReturnsAllMetas()
        {
            var tarefas = new List<Tarefa> { new Tarefa(), new Tarefa() };
            _tarefaRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(tarefas);

            var result = await _tarefaService.GetAllTarefasAsync();

            Assert.Equal(tarefas, result);

            _tarefaRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetTarefasByCategoriaAsync_ReturnsAllMetas()
        {
            var categoria = Categoria.Trabalho;
            var tarefas = new List<Tarefa> { new Tarefa(), new Tarefa() };
            _tarefaRepositoryMock.Setup(repo => repo.GetByCategoriaAsync(categoria))
                .ReturnsAsync(tarefas);

            var result = await _tarefaService.GetTarefasByCategoriaAsync(categoria);

            Assert.Equal(tarefas, result);

            _tarefaRepositoryMock.Verify(repo => repo.GetByCategoriaAsync(categoria), Times.Once);
        }

        [Fact]
        public async Task GetTarefasByStatusAsync_ReturnsAllMetas()
        {
            var status = StatusTarefa.executada;
            var tarefas = new List<Tarefa> { new Tarefa(), new Tarefa() };
            _tarefaRepositoryMock.Setup(repo => repo.GetByStatusAsync(status))
                .ReturnsAsync(tarefas);

            var result = await _tarefaService.GetTarefasByStatusAsync(status);

            Assert.Equal(tarefas, result);

            _tarefaRepositoryMock.Verify(repo => repo.GetByStatusAsync(status), Times.Once);
        }

        [Fact]
        public async Task GetTarefasByCategoriaAndStatusAsync_ReturnsAllMetas()
        {
            var categoria = Categoria.Trabalho;
            var status = StatusTarefa.executada;
            var tarefas = new List<Tarefa> { new Tarefa(), new Tarefa() };
            _tarefaRepositoryMock.Setup(repo => repo.GetTarefasByCategoriaAndStatusAsync(status, categoria))
                .ReturnsAsync(tarefas);

            var result = await _tarefaService.GetTarefasByCategoriaAndStatusAsync(status, categoria);

            Assert.Equal(tarefas, result);

            _tarefaRepositoryMock.Verify(repo => repo.GetTarefasByCategoriaAndStatusAsync(status, categoria), Times.Once);
        }

        [Fact]
        public async Task AddTarefaAsync_AddsTarefa()
        {
            var tarefa = new Tarefa();
            _tarefaRepositoryMock.Setup(repo => repo.AddAsync(tarefa))
                .Returns(Task.CompletedTask);

            await _tarefaService.AddTarefaAsync(tarefa);

            _tarefaRepositoryMock.Verify(repo => repo.AddAsync(tarefa), Times.Once);
        }

        [Fact]
        public async Task UpdateTarefaAsync_UpdatesTarefa()
        {
            var tarefa = new Tarefa();
            _tarefaRepositoryMock.Setup(repo => repo.UpdateAsync(tarefa))
                .Returns(Task.CompletedTask);

            await _tarefaService.UpdateTarefaAsync(tarefa);

            _tarefaRepositoryMock.Verify(repo => repo.UpdateAsync(tarefa), Times.Once);
        }

        [Fact]
        public async Task DeleteTarefaAsync_DeletesTarefa()
        {
            var tarefaId = 1;
            _tarefaRepositoryMock.Setup(repo => repo.DeleteAsync(tarefaId))
                .Returns(Task.CompletedTask);

            await _tarefaService.DeleteTarefaAsync(tarefaId);

            _tarefaRepositoryMock.Verify(repo => repo.DeleteAsync(tarefaId), Times.Once);
        }
    }

}