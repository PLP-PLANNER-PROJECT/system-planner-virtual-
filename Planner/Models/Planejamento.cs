namespace Planner.Models
{
    public class Planejamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        // Lista de tarefas associadas ao planejamento
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();


        
    }
}
