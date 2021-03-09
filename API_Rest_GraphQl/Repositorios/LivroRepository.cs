using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Models.Context;
using API_Rest_GraphQl.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Repositorios
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaContext _context;

        public LivroRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public Livro AdicionarLivro(Livro livro)
        {
            try
            {
                _context.Add(livro);

                return livro;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Livro AtualizarLivro(Livro livro)
        {
            try
            {
                _context.Update(livro);

                return livro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExcluirLivro(Livro livro)
        {
            try
            {
                _context.Remove(livro);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Livro ObterLivro(decimal id)
        {
            try
            {
                return _context.Livros.Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Livro> ObterLivros()
        {
            try
            {
                return _context.Livros.Select(x => x).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
