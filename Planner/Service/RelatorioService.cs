using System;
using System.Threading.Tasks;
using Planner.Models;
using Planner.Repository;

namespace Planner.Service
{
    public class RelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<Relatorio> GerarRelatorioSemanalAsync(DateTime inicioSemana)
        {
            return await _relatorioRepository.GerarRelatorioSemanalAsync(inicioSemana);
        }

        public async Task<Relatorio> GerarRelatorioMensalAsync(DateTime inicioMes)
        {
            return await _relatorioRepository.GerarRelatorioMensalAsync(inicioMes);
        }

        public async Task<Relatorio> GerarRelatorioAnualAsync(DateTime inicioAno)
        {
            return await _relatorioRepository.GerarRelatorioAnualAsync(inicioAno);
        }
    }
}
