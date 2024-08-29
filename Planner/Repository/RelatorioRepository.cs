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

            var porcentagemMetasCumpridas = totalMetas > 0 ? (double)metasCumpridas / totalMetas * 100 : 0;

            // Calcular quantidade e porcentagem de tarefas executadas
            var totalTarefas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioSemana && t.Dia < fimSemana);
            var tarefasExecutadas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioSemana && t.Dia < fimSemana && t.StatusTarefa == StatusTarefa.executada);

            var porcentagemTarefasExecutadas = totalTarefas > 0 ? (double)tarefasExecutadas / totalTarefas * 100 : 0;

            // Identificar semanas mais produtivas
            var semanasMaisProdutivas = "Implementar lógica para identificar semanas mais produtivas"; // Implementar conforme a lógica desejada

            // Identificar turnos mais produtivos
            var turnosMaisProdutivos = "Implementar lógica para identificar turnos mais produtivos"; // Implementar conforme a lógica desejada

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
                QuantidadeMetasCumpridas = metasCumpridas,
                PorcentagemMetasCumpridas = porcentagemMetasCumpridas,
                QuantidadeTarefasExecutadas = tarefasExecutadas,
                PorcentagemTarefasExecutadas = porcentagemTarefasExecutadas,
                //SemanasMaisProdutivas = semanasMaisProdutivas,
                //TurnosMaisProdutivos = turnosMaisProdutivos,
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

            var porcentagemMetasCumpridas = totalMetas > 0 ? (double)metasCumpridas / totalMetas * 100 : 0;

            // Calcular quantidade e porcentagem de tarefas executadas
            var totalTarefas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioMes && t.Dia < fimMes);
            var tarefasExecutadas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioMes && t.Dia < fimMes && t.StatusTarefa == StatusTarefa.executada);

            var porcentagemTarefasExecutadas = totalTarefas > 0 ? (double)tarefasExecutadas / totalTarefas * 100 : 0;

            // Identificar meses mais produtivos
            var mesesMaisProdutivos = "Implementar lógica para identificar meses mais produtivos"; // Implementar conforme a lógica desejada

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
                QuantidadeMetasCumpridas = metasCumpridas,
                PorcentagemMetasCumpridas = porcentagemMetasCumpridas,
                QuantidadeTarefasExecutadas = tarefasExecutadas,
                PorcentagemTarefasExecutadas = porcentagemTarefasExecutadas,
                //SemanasMaisProdutivas = "Implementar lógica para identificar semanas mais produtivas", // Implementar conforme a lógica desejada
                //TurnosMaisProdutivos = "Implementar lógica para identificar turnos mais produtivos", // Implementar conforme a lógica desejada
                CategoriaTarefaMaisRealizada = categoriasMaisRealizadas.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma",
                CategoriaMetaMaisRealizada = categoriasMaisRealizadasMeta.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma"
            };

            return relatorio;
        }


        public async Task<Relatorio> GerarRelatorioAnualAsync(DateTime inicioAno)
        {
            var fimAno = inicioAno.AddYears(1);

            // Calcular quantidade e porcentagem de metas cumpridas
            var totalMetas = await _context.Metas.CountAsync(m => m.Prazo >= inicioAno && m.Prazo < fimAno);
            var metasCumpridas = await _context.Metas.CountAsync(m => m.Prazo >= inicioAno && m.Prazo < fimAno && m.StatusMeta == StatusMeta.sucesso);

            var porcentagemMetasCumpridas = totalMetas > 0 ? (double)metasCumpridas / totalMetas * 100 : 0;

            // Calcular quantidade e porcentagem de tarefas executadas
            var totalTarefas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioAno && t.Dia < fimAno);
            var tarefasExecutadas = await _context.Tarefas.CountAsync(t => t.Dia >= inicioAno && t.Dia < fimAno && t.StatusTarefa == StatusTarefa.executada);

            var porcentagemTarefasExecutadas = totalTarefas > 0 ? (double)tarefasExecutadas / totalTarefas * 100 : 0;

            // Identificar anos mais produtivos
            var anosMaisProdutivos = "Implementar lógica para identificar anos mais produtivos"; // Implementar conforme a lógica desejada

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
                QuantidadeMetasCumpridas = metasCumpridas,
                PorcentagemMetasCumpridas = porcentagemMetasCumpridas,
                QuantidadeTarefasExecutadas = tarefasExecutadas,
                PorcentagemTarefasExecutadas = porcentagemTarefasExecutadas,
                //SemanasMaisProdutivas = "Implementar lógica para identificar semanas mais produtivas", // Implementar conforme a lógica desejada
                //TurnosMaisProdutivos = "Implementar lógica para identificar turnos mais produtivos", // Implementar conforme a lógica desejada
                CategoriaTarefaMaisRealizada = categoriasMaisRealizadas.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma",
                CategoriaMetaMaisRealizada = categoriasMaisRealizadasMeta.FirstOrDefault()?.Categoria.ToString() ?? "Nenhuma"
            };

            return relatorio;
        }

    }
}
