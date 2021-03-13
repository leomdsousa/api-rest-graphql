using API_Rest_GraphQl.Models;
using System.Collections.Generic;

namespace API_Rest_GraphQl.Repositorios.Interfaces
{
    public interface ILivroRepository
    {
        Livro ObterLivro(decimal id);
        List<Livro> ObterLivros();
        Livro AdicionarLivro(Livro livro);
        Livro AtualizarLivro(Livro livro);
        bool ExcluirLivro(Livro livro);
    }
}
