using Microsoft.EntityFrameworkCore;

namespace Planner.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Lembrete> Lembretes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura 'Relatorio' como uma entidade sem chave
            modelBuilder.Entity<Relatorio>().HasNoKey();
        }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
    }
}
