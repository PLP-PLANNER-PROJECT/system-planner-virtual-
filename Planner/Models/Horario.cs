public class Horario
{
    public int Id { get; set; }
    public TimeSpan Inicio { get; set; }
    public TimeSpan Fim { get; set; }
    public List<string> Turnos { get; set; }

    public Horario(TimeSpan inicio, TimeSpan fim)
    {
        if (fim <= inicio)
            throw new ArgumentException("O horário final deve ser posterior ao horário inicial.");

        Inicio = inicio;
        Fim = fim;
        Turnos = DefinirTurnos(inicio, fim);
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

    public override string ToString()
    {
        return $"{Inicio} - {Fim} (Turnos: {string.Join(", ", Turnos)})";
    }
}