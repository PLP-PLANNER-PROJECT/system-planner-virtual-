using Microsoft.AspNetCore.Mvc;
using Planner.Models.Enum;
using Planner.Models;
using Planner.Service;
using System.Threading.Tasks;

namespace Planner.Controllers
{
    [Route("Meta")]
    public class MetaController : Controller
    {
        private readonly MetaService _metaService;

        public MetaController(MetaService metaService)
        {
            _metaService = metaService;
        }

        // GET: /Meta
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Categoria? categoria = null, [FromQuery] StatusMeta? status = null)
        {
            IEnumerable<Meta> metas;

            if (categoria.HasValue)
            {
                metas = await _metaService.GetMetasByCategoriaAsync(categoria.Value);
            }
            else if (status.HasValue)
            {
                metas = await _metaService.GetMetasByStatusAsync(status.Value);
            }
            else
            {
                metas = await _metaService.GetAllMetasAsync();
            }

            return View(metas.ToList()); // Convertendo IEnumerable<Meta> para List<Meta>
        }


        // GET: /Meta/Detalhes/5
        [HttpGet("Detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var meta = await _metaService.GetMetaByIdAsync(id);

            if (meta == null)
            {
                return NotFound();
            }

            return View(meta); // Renderiza a view 'Detalhes' com a meta específica
        }

        // GET: /Meta/Adicionar
        [HttpGet("Adicionar")]
        public IActionResult Adicionar()
        {
            return View(); // Renderiza a view 'Adicionar'
        }

        // POST: /Meta/Adicionar
        [HttpPost("Adicionar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(Meta meta)
        {
            if (ModelState.IsValid)
            {
                await _metaService.AddMetaAsync(meta);
                return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após adicionar a meta
            }

            return View(meta); // Se o modelo não for válido, retorna à view 'Adicionar' com os dados preenchidos
        }

        // GET: /Meta/Editar/5
        [HttpGet("Editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var meta = await _metaService.GetMetaByIdAsync(id);

            if (meta == null)
            {
                return NotFound();
            }

            return View(meta); // Renderiza a view 'Editar' com a meta específica
        }

        // POST: /Meta/Editar/5
        [HttpPost("Editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Meta metaAtualizada)
        {
            if (id != metaAtualizada.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _metaService.UpdateMetaAsync(metaAtualizada);
                return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após atualizar a meta
            }

            return View(metaAtualizada); // Se o modelo não for válido, retorna à view 'Editar' com os dados preenchidos
        }

        // GET: /Meta/Deletar/5
        [HttpGet("Deletar/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var meta = await _metaService.GetMetaByIdAsync(id);

            if (meta == null)
            {
                return NotFound();
            }

            return View(meta); // Renderiza a view 'Deletar' com a meta específica
        }

        // POST: /Meta/Deletar/5
        [HttpPost("Deletar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            var meta = await _metaService.GetMetaByIdAsync(id);
            if (meta == null)
            {
                return NotFound();
            }

            await _metaService.DeleteMetaAsync(id);
            return RedirectToAction(nameof(Index)); // Redireciona para a ação Index após deletar a meta
        }
    }
}
