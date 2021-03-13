using API_Rest_GraphQl.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Rest_GraphQl.Models.Context
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
