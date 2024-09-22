using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Planner.Models;
using Planner.Models.Enum;

namespace Planner.Repository
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly Contexto _context;

        public RelatorioRepository(Contexto context)
        {
            _context = context;
        }

        public async Task<Relatorio> GerarRelatorioSemanalAsync(DateTime inicioSemana)
        {
            var fimSemana = inicioSemana.AddDays(7);

            // Calcular quantidade e porcentagem de metas cumpridas
            var totalMetas = await _context.Metas.CountAsync(m => m.Prazo >= inicioSemana && m.Prazo < fimSemana);
            var metasCumpridas = await _context.Metas.CountAsync(m => m.Prazo >= inicioSemana && m.Prazo < fimSemana && m.StatusMeta == StatusMeta.sucesso);

            var porcentagemMetasCumpridas = (totalMetas > 0 ? (double)metasCumpridas / totalMetas * 100 : 0).ToString("F2");

            // Calcular quantidade e porcentagem de tarefas executadas
            var totalTarefas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioSemana && t.Dia < fimSemana);
            var tarefasExecutadas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioSemana && t.Dia < fimSemana && t.StatusTarefa == StatusTarefa.executada);

            var porcentagemTarefasExecutadas = (totalTarefas > 0 ? (double)tarefasExecutadas / totalTarefas * 100 : 0).ToString("F2");

            var tarefasExecutadasNoPeriodo = await _context.Tarefas
            .Where(t => t.Dia >= inicioSemana && t.Dia < fimSemana && t.StatusTarefa == StatusTarefa.executada)
            .ToListAsync();

            // Processar os turnos em memória após carregar as tarefas
            var tarefasPorTurno = tarefasExecutadasNoPeriodo
                .SelectMany(t => t.Turnos)
                .GroupBy(turno => turno)
                .Select(g => new { Turno = g.Key, Quantidade = g.Count() })
                .OrderByDescending(t => t.Quantidade)
                .ToList();

            var turnoMaisProdutivo = tarefasPorTurno.FirstOrDefault()?.Turno ?? "Nenhum";

            // Identificar categorias mais realizadas Tarefa
            var categoriasMaisRealizadas = await _context.Tarefas
                .Where(t => t.Dia >= inicioSemana && t.Dia < fimSemana)
                .GroupBy(t => t.CategoriaAtividade)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            // Identificar categorias mais realizadas Meta
            var categoriasMaisRealizadasMeta = await _context.Metas
                .Where(m => m.Prazo >= inicioSemana && m.Prazo < fimSemana)
                .GroupBy(m => m.CategoriaAtividade)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                .ToListAsync();


            // Criar o objeto de relatório
            var relatorio = new Relatorio
            {
                QuantidadeMetasCriadas = totalMetas,
                QuantidadeMetasCumpridas = metasCumpridas,
                PorcentagemMetasCumpridas = porcentagemMetasCumpridas,
                QuantidadeMetasNaoCumpridas = totalMetas - metasCumpridas,
                QuantidadeTarefasCriadas = totalTarefas,
                QuantidadeTarefasExecutadas = tarefasExecutadas,
                PorcentagemTarefasExecutadas = porcentagemTarefasExecutadas,
                QuantidadeTarefasNaoExecutadas = totalTarefas - tarefasExecutadas,
                TurnosMaisProdutivos = turnoMaisProdutivo,
                CategoriaTarefaMaisRealizada = categoriasMaisRealizadas.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma",
                CategoriaMetaMaisRealizada = categoriasMaisRealizadasMeta.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma"
            };

            return relatorio;
        }


        public async Task<Relatorio> GerarRelatorioMensalAsync(DateTime inicioMes)
        {
            var fimMes = inicioMes.AddMonths(1);

            // Calcular quantidade e porcentagem de metas cumpridas
            var totalMetas = await _context.Metas.CountAsync(m => m.Prazo >= inicioMes && m.Prazo < fimMes);
            var metasCumpridas = await _context.Metas.CountAsync(m => m.Prazo >= inicioMes && m.Prazo < fimMes && m.StatusMeta == StatusMeta.sucesso);

            var porcentagemMetasCumpridas = (totalMetas > 0 ? (double)metasCumpridas / totalMetas * 100 : 0).ToString("F2");

            // Calcular quantidade e porcentagem de tarefas executadas
            var totalTarefas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioMes && t.Dia < fimMes);
            var tarefasExecutadas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioMes && t.Dia < fimMes && t.StatusTarefa == StatusTarefa.executada);

            var porcentagemTarefasExecutadas = (totalTarefas > 0 ? (double)tarefasExecutadas / totalTarefas * 100 : 0).ToString("F2");

            // Obter as tarefas executadas no período do mês
            var tarefasExecutadasNoPeriodo = await _context.Tarefas
                .Where(t => t.Dia >= inicioMes && t.Dia < fimMes && t.StatusTarefa == StatusTarefa.executada)
                .ToListAsync();

            // Processar os turnos em memória após carregar as tarefas
            var tarefasPorTurno = tarefasExecutadasNoPeriodo
                .SelectMany(t => t.Turnos)
                .GroupBy(turno => turno)
                .Select(g => new { Turno = g.Key, Quantidade = g.Count() })
                .OrderByDescending(t => t.Quantidade)
                .ToList();

            var turnoMaisProdutivo = tarefasPorTurno.FirstOrDefault()?.Turno ?? "Nenhum";

            // Semana mais produtiva
            var semanaMaisProdutiva = await CalcularSemanaMaisProdutivaAsync(inicioMes, fimMes);

            // Identificar categorias mais realizadas
            var categoriasMaisRealizadas = await _context.Tarefas
                .Where(t => t.Dia >= inicioMes && t.Dia < fimMes)
                .GroupBy(t => t.CategoriaAtividade)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            // Identifica a categoria de meta mais realizada
            var categoriasMaisRealizadasMeta = await _context.Metas
                .Where(m => m.Prazo >= inicioMes && m.Prazo < fimMes)
                .GroupBy(m => m.CategoriaAtividade)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            // Criar o objeto de relatório
            var relatorio = new Relatorio
            {
                QuantidadeMetasCriadas = totalMetas,
                QuantidadeMetasCumpridas = metasCumpridas,
                PorcentagemMetasCumpridas = porcentagemMetasCumpridas,
                QuantidadeMetasNaoCumpridas = totalMetas - metasCumpridas,
                QuantidadeTarefasCriadas = totalTarefas,
                QuantidadeTarefasExecutadas = tarefasExecutadas,
                PorcentagemTarefasExecutadas = porcentagemTarefasExecutadas,
                QuantidadeTarefasNaoExecutadas = totalTarefas - tarefasExecutadas,
                SemanasMaisProdutivas = semanaMaisProdutiva,
                TurnosMaisProdutivos = turnoMaisProdutivo,
                CategoriaTarefaMaisRealizada = categoriasMaisRealizadas.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma",
                CategoriaMetaMaisRealizada = categoriasMaisRealizadasMeta.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma"
            };

            return relatorio;
        }
        // Método auxiliar para calcular a semana mais produtiva
        private async Task<string> CalcularSemanaMaisProdutivaAsync(DateTime inicioMes, DateTime fimMes)
        {
            // Inicializa uma variável para armazenar a semana mais produtiva
            string semanaMaisProdutiva = "Nenhuma";
            int maiorProdutividade = 0;

            // Divida o mês em semanas e calcule a produtividade de cada semana
            for (DateTime inicioSemana = inicioMes; inicioSemana < fimMes; inicioSemana = inicioSemana.AddDays(7))
            {
                var fimSemana = inicioSemana.AddDays(7);

                // Contar o número de tarefas executadas na semana
                var tarefasExecutadasSemana = await _context.Tarefas
                    .CountAsync(t => t.Dia >= inicioSemana && t.Dia < fimSemana && t.StatusTarefa == StatusTarefa.executada);

                // Verifica se esta semana foi mais produtiva do que a anterior
                if (tarefasExecutadasSemana > maiorProdutividade)
                {
                    maiorProdutividade = tarefasExecutadasSemana;
                    semanaMaisProdutiva = $"{inicioSemana:dd/MM/yyyy} - {fimSemana:dd/MM/yyyy}";
                }
            }

            return semanaMaisProdutiva;
        }

        // Método auxiliar para calcular o mês mais produtiva
        private async Task<string> CalcularMesMaisProdutivoAsync(DateTime inicioAno, DateTime fimAno)
        {
            // Inicializa uma variável para armazenar o mês mais produtivo
            string mesMaisProdutivo = "Nenhum";
            int maiorProdutividade = 0;

            // Divida o ano em meses e calcule a produtividade de cada mês
            for (DateTime inicioMes = inicioAno; inicioMes < fimAno; inicioMes = inicioMes.AddMonths(1))
            {
                var fimMes = inicioMes.AddMonths(1);

                // Contar o número de tarefas executadas no mês
                var tarefasExecutadasMes = await _context.Tarefas
                    .CountAsync(t => t.Dia >= inicioMes && t.Dia < fimMes && t.StatusTarefa == StatusTarefa.executada);

                // Verifica se este mês foi mais produtivo do que o anterior
                if (tarefasExecutadasMes > maiorProdutividade)
                {
                    maiorProdutividade = tarefasExecutadasMes;
                    mesMaisProdutivo = inicioMes.ToString("MMMM/yyyy");
                }
            }

            return mesMaisProdutivo;
        }

        public async Task<Relatorio> GerarRelatorioAnualAsync(DateTime inicioAno)
        {
            var fimAno = inicioAno.AddYears(1);

            // Calcular quantidade e porcentagem de metas cumpridas
            var totalMetas = await _context.Metas.CountAsync(m => m.Prazo >= inicioAno && m.Prazo < fimAno);
            var metasCumpridas = await _context.Metas.CountAsync(m => m.Prazo >= inicioAno && m.Prazo < fimAno && m.StatusMeta == StatusMeta.sucesso);

            var porcentagemMetasCumpridas = (totalMetas > 0 ? (double)metasCumpridas / totalMetas * 100 : 0).ToString("F2");

            // Calcular quantidade e porcentagem de tarefas executadas
            var totalTarefas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioAno && t.Dia < fimAno);
            var tarefasExecutadas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioAno && t.Dia < fimAno && t.StatusTarefa == StatusTarefa.executada);

            var porcentagemTarefasExecutadas = (totalTarefas > 0 ? (double)tarefasExecutadas / totalTarefas * 100 : 0).ToString("F2");


            // Obter as tarefas executadas no período do mês
            var tarefasExecutadasNoPeriodo = await _context.Tarefas
                .Where(t => t.Dia >= inicioAno && t.Dia < fimAno && t.StatusTarefa == StatusTarefa.executada)
                .ToListAsync();

            // Processar os turnos em memória após carregar as tarefas
            var tarefasPorTurno = tarefasExecutadasNoPeriodo
                .SelectMany(t => t.Turnos)
                .GroupBy(turno => turno)
                .Select(g => new { Turno = g.Key, Quantidade = g.Count() })
                .OrderByDescending(t => t.Quantidade)
                .ToList();

            var turnoMaisProdutivo = tarefasPorTurno.FirstOrDefault()?.Turno ?? "Nenhum";

            // Semanas mais produtivas
            var semanaMaisProdutiva = await CalcularSemanaMaisProdutivaAsync(inicioAno, fimAno);

            // Meses mais produtivos
            var mesMaisProdutivo = await CalcularMesMaisProdutivoAsync(inicioAno, fimAno);

            // Identificar categorias mais realizadas
            var categoriasMaisRealizadas = await _context.Tarefas
                .Where(t => t.Dia >= inicioAno && t.Dia < fimAno)
                .GroupBy(t => t.CategoriaAtividade)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            // Identificar categorias mais realizadas Meta
            var categoriasMaisRealizadasMeta = await _context.Metas
                .Where(m => m.Prazo >= inicioAno && m.Prazo < fimAno)
                .GroupBy(m => m.CategoriaAtividade)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            // Criar o objeto de relatório
            var relatorio = new Relatorio
            {
                QuantidadeMetasCriadas = totalMetas,
                QuantidadeMetasCumpridas = metasCumpridas,
                PorcentagemMetasCumpridas = porcentagemMetasCumpridas,
                QuantidadeMetasNaoCumpridas = totalMetas - metasCumpridas,
                QuantidadeTarefasCriadas = totalTarefas,
                QuantidadeTarefasExecutadas = tarefasExecutadas,
                PorcentagemTarefasExecutadas = porcentagemTarefasExecutadas,
                QuantidadeTarefasNaoExecutadas = totalTarefas - tarefasExecutadas,
                SemanasMaisProdutivas = semanaMaisProdutiva,
                MesesMaisProdutivos = mesMaisProdutivo,
                TurnosMaisProdutivos = turnoMaisProdutivo,
                CategoriaTarefaMaisRealizada = categoriasMaisRealizadas.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma",
                CategoriaMetaMaisRealizada = categoriasMaisRealizadasMeta.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma"
            };

            return relatorio;
        }


        public async Task<Relatorio> GerarRelatorioPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            // Calcular quantidade e porcentagem de metas cumpridas
            var totalMetas = await _context.Metas.CountAsync(m => m.Prazo >= dataInicio && m.Prazo <= dataFim);
            var metasCumpridas = await _context.Metas.CountAsync(m => m.Prazo >= dataInicio && m.Prazo <= dataFim && m.StatusMeta == StatusMeta.sucesso);

            var porcentagemMetasCumpridas = (totalMetas > 0 ? (double)metasCumpridas / totalMetas * 100 : 0).ToString("F2");

            // Calcular quantidade e porcentagem de tarefas executadas
            var totalTarefas = await _context.Tarefas.CountAsync(t => t.Dia >= dataInicio && t.Dia <= dataFim);
            var tarefasExecutadas = await _context.Tarefas.CountAsync(t => t.Dia >= dataInicio && t.Dia <= dataFim && t.StatusTarefa == StatusTarefa.executada);

            var porcentagemTarefasExecutadas = (totalTarefas > 0 ? (double)tarefasExecutadas / totalTarefas * 100 : 0).ToString("F2");

            // Obter as tarefas executadas no período do mês
            var tarefasExecutadasNoPeriodo = await _context.Tarefas
                .Where(t => t.Dia >= dataInicio && t.Dia < dataFim && t.StatusTarefa == StatusTarefa.executada)
                .ToListAsync();

            // Processar os turnos em memória após carregar as tarefas
            var tarefasPorTurno = tarefasExecutadasNoPeriodo
                .SelectMany(t => t.Turnos)
                .GroupBy(turno => turno)
                .Select(g => new { Turno = g.Key, Quantidade = g.Count() })
                .OrderByDescending(t => t.Quantidade)
                .ToList();

            var turnoMaisProdutivo = tarefasPorTurno.FirstOrDefault()?.Turno ?? "Nenhum";

            // Semanas mais produtivas
            var semanaMaisProdutiva = await CalcularSemanaMaisProdutivaAsync(dataInicio, dataFim);

            // Meses mais produtivos
            var mesMaisProdutivo = await CalcularMesMaisProdutivoAsync(dataInicio, dataFim);

            // Identificar categorias mais realizadas Tarefa
            var categoriasMaisRealizadas = await _context.Tarefas
                .Where(t => t.Dia >= dataInicio && t.Dia <= dataFim)
                .GroupBy(t => t.CategoriaAtividade)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            // Identificar categorias mais realizadas Meta
            var categoriasMaisRealizadasMeta = await _context.Metas
                .Where(m => m.Prazo >= dataInicio && m.Prazo <= dataFim)
                .GroupBy(m => m.CategoriaAtividade)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            // Criar o objeto de relatório
            var relatorio = new Relatorio
            {
                QuantidadeMetasCriadas = totalMetas,
                QuantidadeMetasCumpridas = metasCumpridas,
                PorcentagemMetasCumpridas = porcentagemMetasCumpridas,
                QuantidadeMetasNaoCumpridas = totalMetas - metasCumpridas,
                QuantidadeTarefasCriadas = totalTarefas,
                QuantidadeTarefasExecutadas = tarefasExecutadas,
                MesesMaisProdutivos = mesMaisProdutivo,
                SemanasMaisProdutivas = semanaMaisProdutiva,
                TurnosMaisProdutivos = turnoMaisProdutivo,
                PorcentagemTarefasExecutadas = porcentagemTarefasExecutadas,
                QuantidadeTarefasNaoExecutadas = totalTarefas - tarefasExecutadas,
                CategoriaTarefaMaisRealizada = categoriasMaisRealizadas.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma",
                CategoriaMetaMaisRealizada = categoriasMaisRealizadasMeta.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma"
            };

            return relatorio;
        }

    }
}
