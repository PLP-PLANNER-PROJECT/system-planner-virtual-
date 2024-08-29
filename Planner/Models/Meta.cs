using Planner.Models.Enum;

namespace Planner.Models
{
    public class Meta : Atividade
    {
        //TODO: Adicionar verificação de prazo para que seja sempre maior que a data atual
        public DateTime Prazo { get; set; }

        //TODO: 
        public StatusMeta StatusMeta { get; set; }

        // Construtor padrão da classe Tarefa
        public Meta()
        {
        }

        // Construtor da classe Tarefa
        public Meta(int id, string titulo, string descricao, Categoria categoria,DateTime prazo, StatusMeta statusMeta)
        : base(id, titulo, descricao, categoria)
        {
            StatusMeta=statusMeta;
            Prazo = prazo;
        }

        // Opcional: Construtor com parâmetro Descrição opcional
        public Meta(int id, string titulo, Categoria categoria, StatusMeta statusMeta, DateTime prazo, string? descricao = null)
            : base(id, titulo, descricao, categoria)
        {
            StatusMeta = statusMeta;
            Prazo = prazo;
        }
    }

    
}
