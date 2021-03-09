using API_Rest_GraphQl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Services.Interfaces
{
    public interface ILivroService
    {
        Task<Livro> ObterLivro(decimal id);
        Task<List<Livro>> ObterLivros();
        Task<Livro> AdicionarLivro(Livro livro);
        Task<Livro> AtualizarLivro(Livro livro);
        Task<bool> ExcluirLivro(decimal id);
    }
}
