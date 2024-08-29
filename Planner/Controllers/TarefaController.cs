using Microsoft.AspNetCore.Mvc;
using Planner.Models.Enum;
using Planner.Models;
using Planner.Service;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Planner.Controllers
{
    public class TarefaController : Controller
    {
        private readonly TarefaService _tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        // GET: /Tarefa
        public async Task<IActionResult> Index([FromQuery] Categoria? categoria = null, [FromQuery] StatusTarefa? status = null)
        {
            IEnumerable<Tarefa> tarefas;

            if (categoria.HasValue && status.HasValue)
            {
                tarefas = await _tarefaService.GetTarefasByCategoriaAndStatusAsync(status.Value, categoria.Value);
            }
            else if (categoria.HasValue)
            {
                tarefas = await _tarefaService.GetTarefasByCategoriaAsync(categoria.Value);
            }
            else if (status.HasValue)
            {
                tarefas = await _tarefaService.GetTarefasByStatusAsync(status.Value);
            }
            else
            {
                tarefas = await _tarefaService.GetAllTarefasAsync();
            }

            return View(tarefas); // Renderiza a view 'Index' com a lista de tarefas
        }

        // GET: /Tarefa/Detalhes/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa); // Renderiza a view 'Detalhes' com a tarefa específica
        }

        // GET: /Tarefa/Adicionar
        public IActionResult Adicionar()
        {
            return View(); // Renderiza a view 'Adicionar'
        }

        // POST: /Tarefa/Adicionar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                if (tarefa.Dia == default(DateTime))
                {
                    tarefa.Dia = DateTime.Now.Date;
                }

                await _tarefaService.AddTarefaAsync(tarefa);
                return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após adicionar a tarefa
            }

            return View(tarefa); // Se o modelo não for válido, retorna à view 'Adicionar' com os dados preenchidos
        }

        // GET: /Tarefa/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa); // Renderiza a view 'Editar' com a tarefa específica
        }

        // POST: /Tarefa/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Tarefa tarefaAtualizada)
        {
            if (id != tarefaAtualizada.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tarefaService.UpdateTarefaAsync(tarefaAtualizada);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingTarefa = await _tarefaService.GetTarefaByIdAsync(id);
                    if (existingTarefa == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(tarefaAtualizada);
        }



        // GET: /Tarefa/Deletar/5
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa); // Renderiza a view 'Deletar' com a tarefa específica
        }

        // POST: /Tarefa/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            await _tarefaService.DeleteTarefaAsync(id);
            return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após deletar a tarefa
        }

        // POST: /Tarefa/DeletarTodos
        [HttpPost]
        [Route("DeletarTodos")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarTodos()
        {
            await _tarefaService.DeleteAllTarefasAsync();
            return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após deletar todas as tarefas
        }
    }
}
