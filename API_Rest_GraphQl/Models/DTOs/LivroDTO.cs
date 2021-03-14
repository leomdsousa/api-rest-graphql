namespace API_Rest_GraphQl.Models.DTOs
{
    public class LivroDTO
    {
        public decimal Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Autor { get; set; }
        
        public bool Lido { get; set; }

        public Livro ParseLivro(LivroDTO livro)
        {
            return new Livro()
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Lido = livro.Lido,
            };
        }
    }
}
