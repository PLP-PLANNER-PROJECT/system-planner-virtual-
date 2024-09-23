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

        public async Task<IActionResult> Index(DateTime? dataReferencia)
        {
            // Se a data de referência não for fornecida, usa a data atual
            var dataBase = dataReferencia ?? DateTime.Now;

            // Calcula o primeiro e o último dia da semana com base na data de referência
            var primeiroDiaDaSemana = dataBase.AddDays(-(int)dataBase.DayOfWeek + 1);
            var ultimoDiaDaSemana = primeiroDiaDaSemana.AddDays(6);

            // Busca as tarefas para a semana selecionada
            var tarefasSemana = await _tarefaService.GetTarefasPorPeriodoAsync(primeiroDiaDaSemana, ultimoDiaDaSemana);
            var tarefasOrdenadas = tarefasSemana.OrderBy(t => t.Dia).ThenBy(t => t.Inicio).ToList();

            // Busca todas as metas e lembretes (mantém o que já existia)
            var lembretesHoje = await _lembreteService.GetLembretesParaHojeAsync();
            ViewBag.lembretesHoje = lembretesHoje;

            var metas = await _metaService.GetAllMetasAsync();
            ViewBag.MetasFuturas = metas.Where(m => m.Prazo.Date >= DateTime.Today).OrderBy(m => m.Prazo).ToList();

            // Passa a data base para a view para facilitar a navegação
            ViewBag.DataBase = dataBase;

            return View(tarefasOrdenadas);
        }


        [HttpPost]
        public async Task<IActionResult> AlterarStatus(int id, StatusTarefa novoStatus)
        {
            try
            {
                var tarefa = await _tarefaService.GetTarefaByIdAsync(id);

                if (tarefa == null)
                {
                    _logger.LogWarning($"Tarefa com ID {id} não encontrada.");
                    return NotFound("Tarefa não encontrada.");
                }

                // Atualiza o status da tarefa
                tarefa.StatusTarefa = novoStatus;
                await _tarefaService.UpdateTarefaAsync(tarefa);

                _logger.LogInformation($"Status da tarefa {tarefa.Titulo} atualizado para {novoStatus}.");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao alterar o status da tarefa.");
                return StatusCode(500, "Erro interno ao alterar o status da tarefa.");
            }
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
