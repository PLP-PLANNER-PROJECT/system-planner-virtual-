using Planner.Models.Enum;

namespace Planner.Models
{
    public class Tarefa: Atividade
    {
        public BlocoDuracao BlocoDuracao { get; set; }
        public DateTime Dia { get; set; } = DateTime.Now.Date;

        public StatusTarefa StatusTarefa { get; set; }

        // Construtor padrão
        public Tarefa()
        {
        }

        // Construtor da classe Tarefa
        public Tarefa(int id, string titulo, string descricao, Categoria categoria, StatusTarefa statusTarefa, BlocoDuracao blocoDuracao, DateTime dia)
            : base(id, titulo, descricao, categoria) // Chamada ao construtor da classe base
        {

            StatusTarefa = statusTarefa;
            BlocoDuracao = blocoDuracao;
            Dia = dia;
        }

        // Opcional: Construtor com parâmetro Descrição opcional
        public Tarefa(int id, string titulo, Categoria categoria, StatusTarefa? statusTarefa, BlocoDuracao blocoDuracao, DateTime dia , string? descricao = null)
            : base(id, titulo, descricao, categoria) // Chamada ao construtor da classe base
        {
            BlocoDuracao = blocoDuracao;
            Dia = dia;
            statusTarefa = StatusTarefa.NaoIniciada;
        }
    }
}
