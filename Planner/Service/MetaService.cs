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

        public async Task<Meta> GetMetaByIdAsync(int id)
        {
            return await _metaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Meta>> GetAllMetasAsync()
        {
            return await _metaRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Meta>> GetMetasByCategoriaAsync(Categoria categoria)
        {
            return await _metaRepository.GetByCategoriaAsync(categoria);
        }

        public async Task<IEnumerable<Meta>> GetMetasByStatusAsync(StatusMeta status)
        {
            return await _metaRepository.GetByStatusAsync(status);
        }

        public async Task AddMetaAsync(Meta meta)
        {
            await _metaRepository.AddAsync(meta);
        }

        public async Task UpdateMetaAsync(Meta meta)
        {
            await _metaRepository.UpdateAsync(meta);
        }

        public async Task DeleteMetaAsync(int id)
        {
            await _metaRepository.DeleteAsync(id);
        }
    }
}
