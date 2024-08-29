using Microsoft.AspNetCore.Mvc;
using Planner.Service;
using System;

namespace Planner.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly RelatorioService _relatorioService;

        public RelatorioController(RelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        // GET: /Relatorio
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Relatorio/RelatorioSemanal
        public async Task<IActionResult> RelatorioSemanal(DateTime inicioSemana)
        {
            var relatorio = await _relatorioService.GerarRelatorioSemanalAsync(inicioSemana);
            return View("RelatorioSemanal", relatorio);
        }

        // GET: /Relatorio/RelatorioMensal
        public async Task<IActionResult> RelatorioMensal(DateTime mes)
        {
            var relatorio = await _relatorioService.GerarRelatorioMensalAsync(mes);
            return View("RelatorioMensal", relatorio);
        }

        // GET: /Relatorio/RelatorioAnual
        public async Task<IActionResult> RelatorioAnual(DateTime ano)
        {
            var relatorio = await _relatorioService.GerarRelatorioAnualAsync(ano);
            return View("RelatorioAnual", relatorio);
        }
    }
}
