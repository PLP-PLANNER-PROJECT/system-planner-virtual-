using System;

namespace Planner.Models
{
    public class Relatorio
    {
        public DateTime Periodo { get; set; } // Data do período do relatório

        public int QuantidadeMetasCriadas { get; set; }
        public int QuantidadeMetasCumpridas { get; set; }
        public string PorcentagemMetasCumpridas { get; set; }
        public int QuantidadeMetasNaoCumpridas { get; set; }
        public int QuantidadeTarefasCriadas { get; set; }
        public int QuantidadeTarefasExecutadas { get; set; }
        public string PorcentagemTarefasExecutadas { get; set; }
        public int QuantidadeTarefasNaoExecutadas { get; set; }
        public string SemanasMaisProdutivas { get; set; }
        public string MesesMaisProdutivos { get; set; }
        public string TurnosMaisProdutivos { get; set; }
        public string CategoriaTarefaMaisRealizada { get; set; }
        public string CategoriaMetaMaisRealizada { get; set; }
    }
}
