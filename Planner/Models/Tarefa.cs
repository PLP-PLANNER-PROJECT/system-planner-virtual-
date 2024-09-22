using Planner.Models.Enum;
using Planner.Models;
using System.ComponentModel.DataAnnotations;

namespace Planner.Models
{
    public class Tarefa: Atividade
    {
        public DateTime Dia { get; set; } = DateTime.Now.Date;
        public StatusTarefa StatusTarefa { get; set; }
        public TimeSpan Inicio { get; set; } 
        public TimeSpan Fim { get; set; } 
        public List<string> Turnos { get; set; } = new List<string>();
        public BlocoDuracao BlocoDuracao { get; set; }

        // Construtor padrão
        public Tarefa()
        {
        }

        // Construtor da classe Tarefa
        public Tarefa(int id, string titulo, string descricao, Categoria categoria, StatusTarefa statusTarefa,
          DateTime dia)
            : base(id, titulo, descricao, categoria) // Chamada ao construtor da classe base
        {
            StatusTarefa = statusTarefa;
            Dia = dia;
        }
        // Opcional: Construtor com parâmetro Descrição opcional
        public Tarefa(int id, string titulo, Categoria categoria, StatusTarefa? statusTarefa,
         DateTime dia, string? descricao = null)
            : base(id, titulo, descricao, categoria) // Chamada ao construtor da classe base
        {
            Dia = dia;
            statusTarefa = StatusTarefa.NaoIniciada;
        }

        // Método para definir o horário de fim com base no bloco de duração
        public void DefinirHorario(TimeSpan inicio, BlocoDuracao blocoDuracao)
        {
            Inicio = inicio;
            BlocoDuracao = blocoDuracao;
            Fim = CalcularFim(inicio, blocoDuracao);
            Turnos = DefinirTurnos(Inicio, Fim);
        }

        // Método para calcular o fim com base no bloco de duração
        private TimeSpan CalcularFim(TimeSpan inicio, BlocoDuracao blocoDuracao)
        {
            return blocoDuracao switch
            {
                BlocoDuracao.MeiaHora => inicio.Add(TimeSpan.FromMinutes(30)),
                BlocoDuracao.UmaHora => inicio.Add(TimeSpan.FromHours(1)),
                BlocoDuracao.Manha => new TimeSpan(12, 0, 0), // Fim da manhã
                BlocoDuracao.Tarde => new TimeSpan(18, 0, 0), // Fim da tarde
                BlocoDuracao.Noite => new TimeSpan(23, 59, 59), // Fim da noite
                BlocoDuracao.DiaTodo => new TimeSpan(23, 59, 59), // Fim do dia
                _ => throw new ArgumentOutOfRangeException(nameof(blocoDuracao), "Bloco de duração inválido")
            };
        }



        // Método que define em quais turnos a tarefa ocorre
        private List<string> DefinirTurnos(TimeSpan inicio, TimeSpan fim)
        {
            var turnos = new List<string>();

            if (inicio < new TimeSpan(6, 0, 0)) turnos.Add("Madrugada");
            if (inicio < new TimeSpan(12, 0, 0) && fim > new TimeSpan(6, 0, 0)) turnos.Add("Manhã");
            if (inicio < new TimeSpan(18, 0, 0) && fim > new TimeSpan(12, 0, 0)) turnos.Add("Tarde");
            if (fim > new TimeSpan(18, 0, 0)) turnos.Add("Noite");

            return turnos;
        }

        public void Horario(TimeSpan inicio, TimeSpan fim)
        {
        if (fim <= inicio)
            throw new ArgumentException("O horário final deve ser posterior ao horário inicial.");

        Inicio = inicio;
        Fim = fim;
        Turnos = DefinirTurnos(inicio, fim);
        }
    }
}
