using Planner.Service;
using Planner.IRepository;
using Moq;
using Planner.Models;
using Planner.Models.Enum;


namespace TestPlanner.Service
{
    public class MetaServiceTest
    {
        // Definição do MetaService e do Mock para o repositório que será usado nos testes.
        private readonly MetaService _metaService;
        private readonly Mock<IMetaRepository> _metaRepositoryMock;

        // Construtor do teste onde criamos o mock e injetamos no serviço.
        public MetaServiceTest()
        {
            // Inicializa o mock do repositório.
            _metaRepositoryMock = new Mock<IMetaRepository>();
            // Cria uma instância do MetaService passando o mock como dependência.
            _metaService = new MetaService(_metaRepositoryMock.Object);
        }

        //[Fact] define que este método é um teste unitário individual.
        [Fact]
        public async Task GetMetaByIdAsync_ReturnsMeta()
        {
            // Arrange
            var metaId = 1;
            var meta = new Meta { Id = metaId };

            // Configura o mock para retornar a instância de Meta quando o método GetByIdAsync for chamado com o id específico.
            // O método Setup é usado para configurar o comportamento do mock.
            // repo => repo.GetByIdAsync(metaId) é uma expressão lambda que representa o método que será chamado.
            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId))
                .ReturnsAsync(meta);

            // ACT - Executamos a ação a ser testada
            var result = await _metaService.GetMetaByIdAsync(metaId);

            // ASSERT - Verificamos se o comportamento foi conforme o esperado
            Assert.Equal(meta, result);

            // Verifica se o método GetByIdAsync foi chamado exatamente uma vez com o id fornecido.
            _metaRepositoryMock.Verify(repo => repo.GetByIdAsync(metaId), Times.Once);
        }

        [Fact]
        public async Task GetAllMetasAsync_ReturnsAllMetas()
        {
            // Arrange
            var metas = new List<Meta> { new Meta(), new Meta() };
            _metaRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(metas);

            // Act
            var result = await _metaService.GetAllMetasAsync();

            // Assert
            Assert.Equal(metas, result);
            _metaRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once); // Garante que GetAllAsync foi chamado uma vez.
        }

        [Fact]
        public async Task GetMetasByCategoriaAsync_ReturnsMetasByCategoria()
        {
            // Arrange
            var categoria = Categoria.Trabalho;
            var metas = new List<Meta> { new Meta { CategoriaAtividade = categoria } };
            _metaRepositoryMock.Setup(repo => repo.GetByCategoriaAsync(categoria))
                .ReturnsAsync(metas);

            // Act
            var result = await _metaService.GetMetasByCategoriaAsync(categoria);

            // Assert
            Assert.Equal(metas, result);
            _metaRepositoryMock.Verify(repo => repo.GetByCategoriaAsync(categoria), Times.Once);
        }

        [Fact]
        public async Task GetMetasByStatusAsync_ReturnsMetasByStatus()
        {
            // Arrange
            var status = StatusMeta.sucesso;
            var metas = new List<Meta> { new Meta { StatusMeta = status } };
            _metaRepositoryMock.Setup(repo => repo.GetByStatusAsync(status))
                .ReturnsAsync(metas);

            // Act
            var result = await _metaService.GetMetasByStatusAsync(status);

            // Assert
            Assert.Equal(metas, result);
            _metaRepositoryMock.Verify(repo => repo.GetByStatusAsync(status), Times.Once);
        }

        [Fact]
        public async Task AddMetaAsync_AddsMeta()
        {
            // Arrange
            var meta = new Meta();
            _metaRepositoryMock.Setup(repo => repo.AddAsync(meta))
                .Returns(Task.CompletedTask);

            // Act
            await _metaService.AddMetaAsync(meta);

            // Assert
            _metaRepositoryMock.Verify(repo => repo.AddAsync(meta), Times.Once);
        }

        [Fact]
        public async Task UpdateMetaAsync_UpdatesMeta()
        {
            // Arrange
            var meta = new Meta();
            _metaRepositoryMock.Setup(repo => repo.UpdateAsync(meta))
                .Returns(Task.CompletedTask);

            // Act
            await _metaService.UpdateMetaAsync(meta);

            // Assert
            _metaRepositoryMock.Verify(repo => repo.UpdateAsync(meta), Times.Once);
        }

        [Fact]
        public async Task DeleteMetaAsync_DeletesMeta()
        {
            // Arrange
            var metaId = 1;
            _metaRepositoryMock.Setup(repo => repo.DeleteAsync(metaId))
                .Returns(Task.CompletedTask);

            // Act
            await _metaService.DeleteMetaAsync(metaId);

            // Assert
            _metaRepositoryMock.Verify(repo => repo.DeleteAsync(metaId), Times.Once);
        }
    }
}
