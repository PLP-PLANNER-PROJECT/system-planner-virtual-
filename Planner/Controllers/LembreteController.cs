using Microsoft.AspNetCore.Mvc;
using Planner.Models;
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
        public async Task<IActionResult> Index()
        {
            var lembretes = await _lembreteService.GetLembretesAsync();
            return View(lembretes); // Renderiza a view 'Index' com a lista de lembretes
        }

        // GET: /Lembrete/Detalhes/5
        public async Task<IActionResult> Detalhe(int id)
        {
            var lembrete = await _lembreteService.GetLembreteByIdAsync(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            return View(lembrete); // Renderiza a view 'Detalhes' com o lembrete específico
        }

        // GET: /Lembrete/Adicionar
        public IActionResult Adicionar()
        {
            return View(); // Renderiza a view 'Adicionar'
        }

        // POST: /Lembrete/Adicionar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(Lembrete lembrete)
        {
            if (ModelState.IsValid)
            {
                await _lembreteService.AdicionarLembreteAsync(lembrete);
                return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após adicionar o lembrete
            }

            return View(lembrete); // Se o modelo não for válido, retorna à view 'Adicionar' com os dados preenchidos
        }

        // GET: /Lembrete/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var lembrete = await _lembreteService.GetLembreteByIdAsync(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            return View(lembrete); // Renderiza a view 'Editar' com o lembrete específico
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
                return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após atualizar o lembrete
            }

            return View(lembreteAtualizado); // Se o modelo não for válido, retorna à view 'Editar' com os dados preenchidos
        }

        // GET: /Lembrete/Deletar/5
        public async Task<IActionResult> Deletar(int id)
        {
            var lembrete = await _lembreteService.GetLembreteByIdAsync(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            return View(lembrete); // Renderiza a view 'Deletar' com o lembrete específico
        }

        // POST: /Lembrete/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            await _lembreteService.DeletarLembreteAsync(id);
            return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após deletar o lembrete
        }


        // GET: /Lembrete/Processar
        public async Task<IActionResult> Processar()
        {
            await _lembreteService.ProcessarLembretes();
            return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após processar os lembretes
        }
    }
}
