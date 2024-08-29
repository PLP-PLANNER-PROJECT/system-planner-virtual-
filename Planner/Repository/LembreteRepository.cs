using Microsoft.EntityFrameworkCore;
using Planner.IRepository;
using Planner.Models;

namespace Planner.Repository
{
    public class LembreteRepository : ILembreteRepository
    {
        private readonly Contexto _contexto;

        public LembreteRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Lembrete>> GetLembretesAsync()
        {
            return await _contexto.Lembretes.ToListAsync();
        }

        public async Task<Lembrete?> GetLembreteByIdAsync(int id)
        {
            return await _contexto.Lembretes.FindAsync(id);
        }

        public async Task<IEnumerable<Lembrete>> GetLembretesParaHojeAsync()
        {
            var hoje = DateTime.Today;
            return await _contexto.Lembretes
                .Where(l => l.DataHora.Date == hoje)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lembrete>> GetLembretesParaAmanhaAsync()
        {
            var amanha = DateTime.Today.AddDays(1);
            return await _contexto.Lembretes
                .Where(l => l.DataHora.Date == amanha)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lembrete>> GetLembretesParaEstaSemanaAsync()
        {
            var hoje = DateTime.Today;
            var fimDaSemana = hoje.AddDays(7);
            return await _contexto.Lembretes
                .Where(l => l.DataHora.Date >= hoje && l.DataHora.Date <= fimDaSemana)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lembrete>> GetLembretesParaEsteMesAsync()
        {
            var hoje = DateTime.Today;
            var fimDoMes = new DateTime(hoje.Year, hoje.Month, DateTime.DaysInMonth(hoje.Year, hoje.Month));
            return await _contexto.Lembretes
                .Where(l => l.DataHora.Date >= hoje && l.DataHora.Date <= fimDoMes)
                .ToListAsync();
        }

        public async Task AdicionarLembreteAsync(Lembrete lembrete)
        {
            await _contexto.Lembretes.AddAsync(lembrete);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarLembreteAsync(Lembrete lembrete)
        {
            _contexto.Lembretes.Update(lembrete);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarLembreteAsync(int id)
        {
            var lembrete = await GetLembreteByIdAsync(id);
            if (lembrete != null)
            {
                _contexto.Lembretes.Remove(lembrete);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}
