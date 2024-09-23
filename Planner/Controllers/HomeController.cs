using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Planner.Models.Enum;
using Planner.Service;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LembreteService _lembreteService;
        private readonly TarefaService _tarefaService;
        private readonly MetaService _metaService;



        // Injeta o serviço de lembretes
        public HomeController(ILogger<HomeController> logger, LembreteService lembreteService, TarefaService tarefaService, MetaService metaService)
        {
            _logger = logger;
            _lembreteService = lembreteService;
            _tarefaService = tarefaService;
            _metaService = metaService;
        }

        // Busca os lembretes a expirar e passa para a view
        public async Task<IActionResult> Index()
        {
            // Busca os lembretes que vencem hoje ou nas próximas horas
            var lembretesHoje = await _lembreteService.GetLembretesParaHojeAsync();
            ViewBag.lembretesHoje = lembretesHoje;

            var tarefasSemana = await _tarefaService.GetTarefasSemanaAsync();
            var tarefasOrdenadas = tarefasSemana.OrderBy(t => t.Dia).ThenBy(t => t.Inicio).ToList();

            // Busca todas as metas
            var metas = await _metaService.GetAllMetasAsync();
            _logger.LogInformation($"Total de metas retornadas: {metas.Count()}");

            // Log para verificar cada meta
            foreach (var meta in metas)
            {
                _logger.LogInformation($"Meta: {meta.Titulo}, Prazo: {meta.Prazo.ToShortDateString()}");
            }

            // Filtra metas para hoje e próximos dias
            ViewBag.MetasFuturas = metas
                .Where(m => m.Prazo.Date >= DateTime.Today) // Exibe metas que ainda não passaram
                .OrderBy(m => m.Prazo)
                .ToList();



            return View(tarefasOrdenadas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
