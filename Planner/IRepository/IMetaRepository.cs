using Planner.Models.Enum;
using Planner.Models;

namespace Planner.IRepository
{
    public interface IMetaRepository
    {
        Task<Meta> GetByIdAsync(int id);
        Task<IEnumerable<Meta>> GetAllAsync();
        Task<IEnumerable<Meta>> GetByCategoriaAsync(Categoria categoria);
        Task<IEnumerable<Meta>> GetByStatusAsync(StatusMeta status);
        Task AddAsync(Meta meta);
        Task UpdateAsync(Meta meta);
        Task DeleteAsync(int id);
    }
}
