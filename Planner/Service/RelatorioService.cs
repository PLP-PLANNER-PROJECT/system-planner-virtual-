using System;
using Planner.Models;
using Planner.Repository;

namespace Planner.Service
{
    public class RelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService() { }
        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public virtual async Task<Relatorio> GerarRelatorioSemanalAsync(DateTime inicioSemana)
        {
            return await _relatorioRepository.GerarRelatorioSemanalAsync(inicioSemana);
        }

        public virtual async Task<Relatorio> GerarRelatorioMensalAsync(DateTime inicioMes)
        {
            return await _relatorioRepository.GerarRelatorioMensalAsync(inicioMes);
        }

        public virtual async Task<Relatorio> GerarRelatorioAnualAsync(DateTime inicioAno)
        {
            return await _relatorioRepository.GerarRelatorioAnualAsync(inicioAno);
        }

        public virtual async Task<Relatorio> GerarRelatorioPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _relatorioRepository.GerarRelatorioPorPeriodoAsync(dataInicio, dataFim);
        }
    }
}
