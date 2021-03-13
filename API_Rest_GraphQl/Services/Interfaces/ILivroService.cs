using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Services.Interfaces
{
    public interface ILivroService
    {
        Task<LivroDTO> ObterLivro(decimal id);
        Task<List<LivroDTO>> ObterLivros();
        Task<LivroDTO> AdicionarLivro(LivroDTO livro);
        Task<LivroDTO> AtualizarLivro(LivroDTO livro);
        Task<bool> ExcluirLivro(decimal id);
    }
}
