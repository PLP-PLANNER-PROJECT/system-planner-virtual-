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
         DateTime dia , string? descricao = null)
            : base(id, titulo, descricao, categoria) // Chamada ao construtor da classe base
        {
            Dia = dia;
            statusTarefa = StatusTarefa.NaoIniciada;
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
