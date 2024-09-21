using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Planner.Service;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LembreteService _lembreteService;

        // Injeta o serviço de lembretes
        public HomeController(ILogger<HomeController> logger, LembreteService lembreteService)
        {
            _logger = logger;
            _lembreteService = lembreteService;
        }

        // Busca os lembretes a expirar e passa para a view
        public async Task<IActionResult> Index()
        {
            // Busca os lembretes que vencem hoje ou nas próximas horas
            var lembretesHoje = await _lembreteService.GetLembretesParaHojeAsync();
            ViewBag.lembretesHoje = lembretesHoje;
            return View();
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
