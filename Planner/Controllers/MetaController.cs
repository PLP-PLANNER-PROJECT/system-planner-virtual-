using Microsoft.AspNetCore.Mvc;
using Planner.Models.Enum;
using Planner.Models;
using Planner.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Index([FromQuery] Categoria? categoria = null, [FromQuery] StatusMeta? status = null, [FromQuery] int? mes = null, [FromQuery] int? ano = null)
        {
            IEnumerable<Meta> metas = await _metaService.GetAllMetasAsync();

            if (categoria.HasValue && status.HasValue)
            {
                metas = metas.Where(m => m.CategoriaAtividade == categoria.Value && m.StatusMeta == status.Value);
            }
            else if (categoria.HasValue)
            {
                metas = metas.Where(m => m.CategoriaAtividade == categoria.Value);
            }
            else if (status.HasValue)
            {
                metas = metas.Where(m => m.StatusMeta == status.Value);
            }

            // Filtro por mês e ano
            if (mes.HasValue)
            {
                metas = metas.Where(m => m.Prazo.Month == mes.Value);
            }

            if (ano.HasValue)
            {
                metas = metas.Where(m => m.Prazo.Year == ano.Value);
            }

            // Adicionar filtro para exibir apenas as metas do dia atual
            var metasHoje = metas
                .Where(m => m.Prazo.Date >= DateTime.Today) // Exibe metas que ainda não passaram
                .OrderBy(m => m.Prazo) // Ordena pelo prazo
                .ToList();


            ViewBag.MetasHoje = metasHoje; // Passando para a ViewBag

            return View(metasHoje); // Retornando apenas as metas do dia
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
                if (meta.Prazo == default(DateTime))
                {
                    meta.Prazo = DateTime.Now.Date;
                }

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

            return RedirectToAction(nameof(Index)); // Redireciona para o Index após deletar
        }
    }
}