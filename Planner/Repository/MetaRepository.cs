using Microsoft.EntityFrameworkCore;
using Planner.IRepository;
using Planner.Models.Enum;
using Planner.Models;


namespace Planner.Repository
{
    public class MetaRepository : IMetaRepository
    {
        private readonly Contexto _context;

        public MetaRepository(Contexto context)
        {
            _context = context;
        }

        public async Task<Meta> GetByIdAsync(int id)
        {
            
            return await _context.Metas.FindAsync(id);
        }

        public async Task<IEnumerable<Meta>> GetAllAsync()
        {
            return await _context.Metas.ToListAsync();
        }

        public async Task<IEnumerable<Meta>> GetByCategoriaAsync(Categoria categoria)
        {
            return await _context.Metas
                .Where(m => m.CategoriaAtividade == categoria)
                .ToListAsync();
        }

        public async Task<IEnumerable<Meta>> GetByStatusAsync(StatusMeta status)
        {
            return await _context.Metas
                .Where(m => m.StatusMeta == status)
                .ToListAsync();
        }

        public async Task AddAsync(Meta meta)
        {
            await _context.Metas.AddAsync(meta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meta meta)
        {
            _context.Metas.Update(meta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var meta = await _context.Metas.FindAsync(id);
            if (meta != null)
            {
                _context.Metas.Remove(meta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
