using Moq;
using Planner.IRepository;
using Planner.Models;
using Planner.Service;


namespace TestPlanner.Service
{
    public class LembreteServiceTests
    {
        // Instâncias do LembreteService e o mock do ILembreteRepository.
        private readonly LembreteService _lembreteService;
        private readonly Mock<ILembreteRepository> _lembreteRepositoryMock;

        // Construtor do teste onde o mock do repositório é inicializado e injetado no serviço.
        public LembreteServiceTests()
        {
            _lembreteRepositoryMock = new Mock<ILembreteRepository>();
            _lembreteService = new LembreteService(_lembreteRepositoryMock.Object);
        }

        // Teste para verificar se o método GetLembretesAsync retorna e ordena os lembretes corretamente.
        [Fact]
        public async Task GetLembretesAsync_ReturnsOrderedLembretes()
        {
            // ARRANGE - Configurando o mock para retornar uma lista de lembretes não ordenada.
            var lembretes = new List<Lembrete>
            {
                new Lembrete { DataHora = new DateTime(2024, 10, 5) },
                new Lembrete { DataHora = new DateTime(2024, 9, 21) }
            };
            _lembreteRepositoryMock.Setup(repo => repo.GetLembretesAsync())
                .ReturnsAsync(lembretes); // Configura o retorno simulado.

            // ACT - Chama o método de serviço que estamos testando.
            var result = await _lembreteService.GetLembretesAsync();

            // ASSERT - Verifica se a lista retornada está ordenada por DataHora.
            Assert.Equal(lembretes.OrderBy(l => l.DataHora), result); // Verifica a ordenação.
            _lembreteRepositoryMock.Verify(repo => repo.GetLembretesAsync(), Times.Once); // Garante que o método foi chamado uma vez.
        }

        // Teste para verificar se GetLembreteByIdAsync retorna o lembrete correto pelo ID.
        [Fact]
        public async Task GetLembreteByIdAsync_ReturnsLembreteById()
        {
            // ARRANGE - Configura o mock para retornar um lembrete específico pelo ID.
            var lembreteId = 1;
            var lembrete = new Lembrete { Id = lembreteId };
            _lembreteRepositoryMock.Setup(repo => repo.GetLembreteByIdAsync(lembreteId))
                .ReturnsAsync(lembrete);

            // ACT - Chama o método de serviço.
            var result = await _lembreteService.GetLembreteByIdAsync(lembreteId);

            // ASSERT - Verifica se o lembrete retornado é o esperado.
            Assert.Equal(lembrete, result);
            _lembreteRepositoryMock.Verify(repo => repo.GetLembreteByIdAsync(lembreteId), Times.Once);
        }

        // Teste para garantir que o método AdicionarLembreteAsync adiciona corretamente um lembrete.
        [Fact]
        public async Task AdicionarLembreteAsync_AddsLembrete()
        {
            // ARRANGE - Cria um lembrete de exemplo.
            var lembrete = new Lembrete();
            _lembreteRepositoryMock.Setup(repo => repo.AdicionarLembreteAsync(lembrete))
                .Returns(Task.CompletedTask); // Configura a simulação.

            // ACT - Chama o método de serviço para adicionar o lembrete.
            await _lembreteService.AdicionarLembreteAsync(lembrete);

            // ASSERT - Verifica se o método foi chamado corretamente.
            _lembreteRepositoryMock.Verify(repo => repo.AdicionarLembreteAsync(lembrete), Times.Once);
        }

        // Teste para verificar se AtualizarLembreteAsync atualiza corretamente um lembrete.
        [Fact]
        public async Task AtualizarLembreteAsync_UpdatesLembrete()
        {
            // ARRANGE - Cria um lembrete de exemplo.
            var lembrete = new Lembrete();
            _lembreteRepositoryMock.Setup(repo => repo.AtualizarLembreteAsync(lembrete))
                .Returns(Task.CompletedTask); // Simula a atualização.

            // ACT - Chama o método de serviço para atualizar o lembrete.
            await _lembreteService.AtualizarLembreteAsync(lembrete);

            // ASSERT - Verifica se o método foi chamado corretamente.
            _lembreteRepositoryMock.Verify(repo => repo.AtualizarLembreteAsync(lembrete), Times.Once);
        }

        // Teste para garantir que o método DeletarLembreteAsync deleta o lembrete corretamente.
        [Fact]
        public async Task DeletarLembreteAsync_DeletesLembrete()
        {
            // ARRANGE - Define o ID do lembrete a ser deletado.
            var lembreteId = 1;
            _lembreteRepositoryMock.Setup(repo => repo.DeletarLembreteAsync(lembreteId))
                .Returns(Task.CompletedTask); // Simula a exclusão.

            // ACT - Chama o método de serviço para deletar o lembrete.
            await _lembreteService.DeletarLembreteAsync(lembreteId);

            // ASSERT - Verifica se o método foi chamado corretamente.
            _lembreteRepositoryMock.Verify(repo => repo.DeletarLembreteAsync(lembreteId), Times.Once);
        }

        // Teste para verificar se o método GetLembretesParaHojeAsync retorna os lembretes de hoje.
        [Fact]
        public async Task GetLembretesParaHojeAsync_ReturnsLembretesForToday()
        {
            // ARRANGE - Simula o retorno de lembretes para hoje.
            var lembretes = new List<Lembrete> { new Lembrete { DataHora = DateTime.Today } };
            _lembreteRepositoryMock.Setup(repo => repo.GetLembretesParaHojeAsync())
                .ReturnsAsync(lembretes);

            // ACT - Chama o método de serviço.
            var result = await _lembreteService.GetLembretesParaHojeAsync();

            // ASSERT - Verifica se os lembretes retornados são os esperados.
            Assert.Equal(lembretes, result);
            _lembreteRepositoryMock.Verify(repo => repo.GetLembretesParaHojeAsync(), Times.Once);
        }

        // Teste para verificar se ProcessarLembretes atualiza lembretes recorrentes.
        [Fact]
        public async Task ProcessarLembretes_UpdatesRecorrentes()
        {
            // ARRANGE - Cria um lembrete recorrente semanal para o teste.
            var lembrete = new Lembrete { RecorrenteSemanal = true, DataHora = DateTime.Today };
            var lembretes = new List<Lembrete> { lembrete };

            // Simula a obtenção de lembretes para hoje.
            _lembreteRepositoryMock.Setup(repo => repo.GetLembretesParaHojeAsync())
                .ReturnsAsync(lembretes);

            // Simula a atualização do lembrete.
            _lembreteRepositoryMock.Setup(repo => repo.AtualizarLembreteAsync(lembrete))
                .Returns(Task.CompletedTask);

            // ACT - Chama o método de serviço para processar lembretes.
            await _lembreteService.ProcessarLembretes();

            // ASSERT - Verifica se o lembrete foi atualizado com a nova data.
            Assert.Equal(DateTime.Today.AddDays(7), lembrete.DataHora); // Verifica se a data foi atualizada para a próxima semana.
            _lembreteRepositoryMock.Verify(repo => repo.AtualizarLembreteAsync(lembrete), Times.Once);
        }
    }
}
