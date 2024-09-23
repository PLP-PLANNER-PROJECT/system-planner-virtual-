using Planner.IRepository;
using Planner.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Service
{
    public class LembreteService
    {
        private readonly ILembreteRepository _lembreteRepository;

        public LembreteService(ILembreteRepository lembreteRepository)
        {
            _lembreteRepository = lembreteRepository;
        }

        // Obtém todos os lembretes e os ordena por DataHora
        public async Task<IEnumerable<Lembrete>> GetLembretesAsync()
        {
            var lembretes = await _lembreteRepository.GetLembretesAsync();
            return lembretes.OrderBy(l => l.DataHora);
        }

        // Obtém um lembrete pelo ID
        public async Task<Lembrete?> GetLembreteByIdAsync(int id)
        {
            return await _lembreteRepository.GetLembreteByIdAsync(id);
        }

        // Processa os lembretes de hoje e notifica o usuário
        public async Task ProcessarLembretes()
        {
            var lembretes = await GetLembretesParaHojeAsync(); // Obtém lembretes para hoje

            foreach (var lembrete in lembretes)
            {
                // Notifica o usuário sobre o lembrete
                NotificarUsuario(lembrete.Titulo);

                if (lembrete.RecorrenteSemanal)
                {
                    // Atualiza a data do lembrete para a próxima semana
                    lembrete.DataHora = lembrete.DataHora.AddDays(7);
                    
                    // Atualiza o lembrete no repositório
                    await AtualizarLembreteAsync(lembrete);
                }
            }
        }

        //GetLembretesPorPeriodoAsync
        public async Task<IEnumerable<Lembrete>> GetLembretesPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _lembreteRepository.GetLembretesPorPeriodoAsync(dataInicio, dataFim);
        }

        // Método de notificação de lembrete
        public void NotificarUsuario(string titulo)
        {
            Console.WriteLine($"🔔 Atenção! Seu lembrete \"{titulo}\" está prestes a expirar. Lembre-se de que ele é recorrente e será notificado semanalmente.");
        }

        // Obtém os lembretes do dia de hoje
        public async Task<IEnumerable<Lembrete>> GetLembretesParaHojeAsync()
        {
            return await _lembreteRepository.GetLembretesParaHojeAsync();
        }

        // Adiciona um novo lembrete
        public async Task AdicionarLembreteAsync(Lembrete lembrete)
        {
            await _lembreteRepository.AdicionarLembreteAsync(lembrete);
        }

        // Atualiza um lembrete existente
        public async Task AtualizarLembreteAsync(Lembrete lembrete)
        {
            await _lembreteRepository.AtualizarLembreteAsync(lembrete);
        }

        // Deleta um lembrete pelo ID
        public async Task DeletarLembreteAsync(int id)
        {
            await _lembreteRepository.DeletarLembreteAsync(id);
        }

        // Métodos adicionais para organizar lembretes
        public async Task<IEnumerable<Lembrete>> GetLembretesParaAmanhaAsync()
        {
            return await _lembreteRepository.GetLembretesParaAmanhaAsync();
        }

        public async Task<IEnumerable<Lembrete>> GetLembretesParaEstaSemanaAsync()
        {
            return await _lembreteRepository.GetLembretesParaEstaSemanaAsync();
        }

        public async Task<IEnumerable<Lembrete>> GetLembretesParaEsteMesAsync()
        {
            return await _lembreteRepository.GetLembretesParaEsteMesAsync();
        }
    }
}
