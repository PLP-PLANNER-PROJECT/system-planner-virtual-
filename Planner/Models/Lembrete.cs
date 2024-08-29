using Planner.Models.Enum;

namespace Planner.Models
{
    public class Lembrete
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public TipoLembrete TipoLembrete { get; set; }


        //RecorrenteSemanal: Indica se o lembrete é recorrente semanalmente, se for, a data será adicionada a cada semana (+7 dias) na função processarLembrete
        public bool RecorrenteSemanal { get; set; }
        public DateTime DataHora { get; set; }


        //TODO: Implementar ou não frequência de lembrete

        

        // Construtor da classe Lembrete
        public Lembrete(int id, string titulo, string? descricao, TipoLembrete tipoLembrete)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            TipoLembrete = tipoLembrete;
        }

        // Construtor da classe Lembrete
        public Lembrete() { }
    }
}
