using Planner.Models;
using System;
using System.Threading.Tasks;

namespace Planner.Repository
{
    public interface IRelatorioRepository
    {
        Task<Relatorio> GerarRelatorioSemanalAsync(DateTime inicioSemana);
        Task<Relatorio> GerarRelatorioMensalAsync(DateTime inicioMes);
        Task<Relatorio> GerarRelatorioAnualAsync(DateTime inicioAno);
    }
}
