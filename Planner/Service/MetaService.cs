using Planner.IRepository;
using Planner.Models.Enum;
using Planner.Models;

namespace Planner.Service
{
    public class MetaService
    {
        private readonly IMetaRepository _metaRepository;

        public MetaService(IMetaRepository metaRepository)
        {
            _metaRepository = metaRepository;
        }

        public virtual async Task<Meta> GetMetaByIdAsync(int id)
        {
            return await _metaRepository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<Meta>> GetAllMetasAsync()
        {
            return await _metaRepository.GetAllAsync();
        }

        public virtual async Task<IEnumerable<Meta>> GetMetasByCategoriaAsync(Categoria categoria)
        {
            return await _metaRepository.GetByCategoriaAsync(categoria);
        }

        public virtual async Task<IEnumerable<Meta>> GetMetasByStatusAsync(StatusMeta status)
        {
            return await _metaRepository.GetByStatusAsync(status);
        }

        public virtual async Task AddMetaAsync(Meta meta)
        {
            await _metaRepository.AddAsync(meta);
        }

        public virtual async Task UpdateMetaAsync(Meta meta)
        {
            await _metaRepository.UpdateAsync(meta);
        }

        public virtual async Task DeleteMetaAsync(int id)
        {
            await _metaRepository.DeleteAsync(id);
        }
    }
}
