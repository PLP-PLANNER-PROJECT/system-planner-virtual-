using Planner.Models.Enum;
using Planner.Models;

namespace Planner.IRepository
{
    public interface ITarefaRepository
    {
        Task<Tarefa> GetByIdAsync(int id);
        Task<IEnumerable<Tarefa>> GetAllAsync();
        Task<IEnumerable<Tarefa>> GetByCategoriaAsync(Categoria categoria);

        Task<IEnumerable<Tarefa>> GetByStatusAsync(StatusTarefa status);

        Task<IEnumerable<Tarefa>> GetTarefasByCategoriaAndStatusAsync(StatusTarefa status, Categoria categoria);

        //Task<IEnumerable<Tarefa>> GetByDataAsync(DateTime data);

        Task AddAsync(Tarefa tarefa);
        Task UpdateAsync(Tarefa tarefa);
        Task DeleteAsync(int id);

        Task DeleteAllAsync();
    }
}
