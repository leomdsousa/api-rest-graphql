using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repository;
        public LivroService(ILivroRepository repository)
        {
            _repository = repository;
        }

        public async Task<Livro> ObterLivro(decimal id)
        {
            try
            {
                return _repository.ObterLivro(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Livro>> ObterLivros()
        {
            try
            {
                return _repository.ObterLivros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Livro> AdicionarLivro(Livro livro)
        {
            try
            {
                return _repository.AdicionarLivro(livro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Livro> AtualizarLivro(Livro livro)
        {
            try
            {
                return _repository.AtualizarLivro(livro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirLivro(decimal id)
        {
            try
            {
                var livro = _repository.ObterLivro(id);

                if(livro == null)
                {
                    return false;
                }

                return _repository.ExcluirLivro(livro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
