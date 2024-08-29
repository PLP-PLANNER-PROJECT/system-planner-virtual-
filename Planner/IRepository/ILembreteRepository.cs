using Planner.Models;

namespace Planner.IRepository
{
    public interface ILembreteRepository
    {
        Task<IEnumerable<Lembrete>> GetLembretesAsync();
        Task<Lembrete?> GetLembreteByIdAsync(int id);
        Task<IEnumerable<Lembrete>> GetLembretesParaHojeAsync();
        Task<IEnumerable<Lembrete>> GetLembretesParaAmanhaAsync();
        Task<IEnumerable<Lembrete>> GetLembretesParaEstaSemanaAsync();
        Task<IEnumerable<Lembrete>> GetLembretesParaEsteMesAsync();
        Task AdicionarLembreteAsync(Lembrete lembrete);
        Task AtualizarLembreteAsync(Lembrete lembrete);
        Task DeletarLembreteAsync(int id);
    }
}
