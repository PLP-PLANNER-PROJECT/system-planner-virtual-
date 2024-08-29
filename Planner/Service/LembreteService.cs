using Planner.IRepository;
using Planner.Models;

namespace Planner.Service
{
    public class LembreteService
    {
        private readonly ILembreteRepository _lembreteRepository;

        public LembreteService(ILembreteRepository lembreteRepository)
        {
            _lembreteRepository = lembreteRepository;
        }

        public async Task<IEnumerable<Lembrete>> GetLembretesAsync()
        {
            return await _lembreteRepository.GetLembretesAsync();
        }

        public async Task<Lembrete?> GetLembreteByIdAsync(int id)
        {
            return await _lembreteRepository.GetLembreteByIdAsync(id);
        }

        //Hoje
        public async Task<IEnumerable<Lembrete>> GetLembretesParaHojeAsync()
        {
            return await _lembreteRepository.GetLembretesParaHojeAsync();
        }

        //Amanhã
        public async Task<IEnumerable<Lembrete>> GetLembretesParaAmanhaAsync()
        {
            return await _lembreteRepository.GetLembretesParaAmanhaAsync();
        }

        //Esta semana
        public async Task<IEnumerable<Lembrete>> GetLembretesParaEstaSemanaAsync()
        {
            return await _lembreteRepository.GetLembretesParaEstaSemanaAsync();
        }

        //Este mês
        public async Task<IEnumerable<Lembrete>> GetLembretesParaEsteMesAsync()
        {
            return await _lembreteRepository.GetLembretesParaEsteMesAsync();
        }

        public async Task AdicionarLembreteAsync(Lembrete lembrete)
        {
            await _lembreteRepository.AdicionarLembreteAsync(lembrete);
        }

        public async Task AtualizarLembreteAsync(Lembrete lembrete)
        {
            await _lembreteRepository.AtualizarLembreteAsync(lembrete);
        }

        public async Task DeletarLembreteAsync(int id)
        {
            await _lembreteRepository.DeletarLembreteAsync(id);
        }

        public async Task ProcessarLembretes()
        {
            var lembretes = await GetLembretesParaHojeAsync();

            foreach (var lembrete in lembretes)
            {
                //TODO: Lógica para notificar o usuário sobre o lembrete

                if (lembrete.RecorrenteSemanal)
                {
                    // Atualiza a data do lembrete para a próxima semana
                    lembrete.DataHora = lembrete.DataHora.AddDays(7);
                    await AtualizarLembreteAsync(lembrete);
                }
            }
        }
    }
}
