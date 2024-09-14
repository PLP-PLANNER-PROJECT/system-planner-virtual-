using System;

namespace Planner.Models
{
    public class Relatorio
    {
        public DateTime Periodo { get; set; } // Data do período do relatório

        public int QuantidadeMetasCriadas { get; set; }
        public int QuantidadeMetasCumpridas { get; set; }
        public double PorcentagemMetasCumpridas { get; set; }
        public int QuantidadeMetasNaoCumpridas { get; set; }
        public int QuantidadeTarefasCriadas { get; set; }
        public int QuantidadeTarefasExecutadas { get; set; }
        public double PorcentagemTarefasExecutadas { get; set; }
        public int QuantidadeTarefasNaoExecutadas { get; set; }
        public int SemanasMaisProdutivas { get; set; }
        public int MesesMaisProdutivos { get; set; }
        public int TurnosMaisProdutivos { get; set; }
        public string CategoriaTarefaMaisRealizada { get; set; }
        public string CategoriaMetaMaisRealizada { get; set; }
    }
}
