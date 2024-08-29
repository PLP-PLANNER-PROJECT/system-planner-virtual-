using Planner.Models.Enum;

namespace Planner.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        //pode ser null
        public string? Descricao { get; set; }
        public Categoria CategoriaAtividade { get; set; }
        

        // Construtor padrão
        public Atividade()
        {
        }

        // Construtor que não exige Descrição
        public Atividade(int id, string titulo, Categoria categoria)
        {
            Id = id;
            Titulo = titulo;
            CategoriaAtividade = categoria;
          
        }

        // Construtor que aceita Descrição
        public Atividade(int id, string titulo, string? descricao, Categoria categoria)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            CategoriaAtividade = categoria;
            
        }
    }

}
