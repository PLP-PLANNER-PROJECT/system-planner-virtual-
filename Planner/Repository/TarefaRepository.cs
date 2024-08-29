using Planner.IRepository;
using Planner.Models.Enum;
using Planner.Models;
using Microsoft.EntityFrameworkCore;

namespace Planner.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly Contexto _context;

        public TarefaRepository(Contexto context)
        {
            _context = context;
        }

        public async Task<Tarefa> GetByIdAsync(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetByCategoriaAsync(Categoria categoria)
        {
            return await _context.Tarefas
                .Where(t => t.CategoriaAtividade == categoria)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetByStatusAsync(StatusTarefa status)
        {
            return await _context.Tarefas
                .Where(t => t.StatusTarefa == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasByCategoriaAndStatusAsync(StatusTarefa status, Categoria categoria)
        {
            return await _context.Tarefas
                .Where(t => t.StatusTarefa == status && t.CategoriaAtividade == categoria)
                .ToListAsync();
        }

        //public async Task<IEnumerable<Tarefa>> GetByDataAsync(DateTime data)
        //{
        //    // filtrar tarefas baseadas na data associada ao planejamento
        //    // Pode precisar ajustar isso dependendo de como armazena a data em tarefas
        //    return await _context.Tarefas
        //        .Where(t => t.Planejamento != null && t.Planejamento.Data == data)
        //        .ToListAsync();
        //}

        public async Task AddAsync(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllAsync()
        {
            _context.Tarefas.RemoveRange(_context.Tarefas);
            await _context.SaveChangesAsync();
        }
    }
}
