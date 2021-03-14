using API_Rest_GraphQl.Models.DTOs;
using API_Rest_GraphQl.Models.Entities;

namespace API_Rest_GraphQl.Models
{
    public class Livro : Comum
    {
        public decimal Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public bool Lido { get; set; }
        public LivroDTO ParseLivroDTo(Livro livro)
        {
            return new LivroDTO()
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Lido = livro.Lido,
            };
        }
    }

}
