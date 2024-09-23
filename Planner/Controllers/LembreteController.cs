using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Planner.Models.Enum;
using Planner.Service;
using System.Threading.Tasks;

namespace Planner.Controllers
{
    public class LembreteController : Controller
    {
        private readonly LembreteService _lembreteService;

        public LembreteController(LembreteService lembreteService)
        {
            _lembreteService = lembreteService;
        }

        // GET: /Lembrete
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] TipoLembrete? tipo = null, [FromQuery] bool? recorrente = null, [FromQuery] int? mes = null, [FromQuery] int? ano = null)
        {
            var lembretes = await _lembreteService.GetLembretesAsync();

            // Filtro por tipo
            if (tipo.HasValue)
            {
                lembretes = lembretes.Where(l => l.TipoLembrete == tipo.Value);
            }

            // Filtro por recorrência
            if (recorrente.HasValue)
            {
                lembretes = lembretes.Where(l => l.RecorrenteSemanal == recorrente.Value);
            }

            // Filtro por mês e ano
            if (mes.HasValue)
            {
                lembretes = lembretes.Where(l => l.DataHora.Month == mes.Value);
            }

            if (ano.HasValue)
            {
                lembretes = lembretes.Where(l => l.DataHora.Year == ano.Value);
            }

            return View(lembretes);
        }


        // GET: /Lembrete/Detalhes/5
        public async Task<IActionResult> Detalhe(int id)
        {
            var lembrete = await _lembreteService.GetLembreteByIdAsync(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            return View(lembrete);
        }

        // GET: /Lembrete/Adicionar
        public IActionResult Adicionar()
        {
            return View();
        }

        // POST: /Lembrete/Adicionar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(Lembrete lembrete)
        {
            if (ModelState.IsValid)
            {
                await _lembreteService.AdicionarLembreteAsync(lembrete);
                return RedirectToAction(nameof(Index));
            }

            return View(lembrete);
        }

        // GET: /Lembrete/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var lembrete = await _lembreteService.GetLembreteByIdAsync(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            return View(lembrete);
        }

        // POST: /Lembrete/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Lembrete lembreteAtualizado)
        {
            if (id != lembreteAtualizado.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _lembreteService.AtualizarLembreteAsync(lembreteAtualizado);
                return RedirectToAction(nameof(Index));
            }

            return View(lembreteAtualizado);
        }

        // GET: /Lembrete/Deletar/5
        public async Task<IActionResult> Deletar(int id)
        {
            var lembrete = await _lembreteService.GetLembreteByIdAsync(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            return View(lembrete);
        }

        // POST: /Lembrete/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            await _lembreteService.DeletarLembreteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Lembrete/Processar
        public async Task<IActionResult> Processar()
        {
            await _lembreteService.ProcessarLembretes();
            return RedirectToAction(nameof(Index));
        }
    }
}
