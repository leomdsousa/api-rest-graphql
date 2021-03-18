using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Models.DTOs;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repository;
        private readonly Utilities.Mapper _mapper;

        public LivroService(ILivroRepository repository, Utilities.Mapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LivroDTO> ObterLivro(decimal id)
        {
            try
            {
                var livro = await Task.Run(() => _repository.ObterLivro(id));

                if(livro == null)
                {
                    return null;
                }

                return _mapper.mapper.Map<LivroDTO>(livro); ;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<LivroDTO>> ObterLivros()
        {
            try
            {
                var livros = await Task.Run(() => _repository.ObterLivros());

                if(livros == null)
                {
                    return null;
                }

                var retorno = new List<LivroDTO>();

                foreach(var item in livros)
                {
                    retorno.Add(item.ParseLivroDTo(item));
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<LivroDTO> AdicionarLivro(LivroDTO livro)
        {
            try
            {
                Livro input = _mapper.mapper.Map<Livro>(livro);
                input.DataInclusao = DateTime.Now;
                input.UsuarioInclusao = 1;

                var result = await Task.Run(() => _repository.AdicionarLivro(input));

                if(result == null)
                {
                    return null;
                }

                return _mapper.mapper.Map<LivroDTO>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LivroDTO> AtualizarLivro(LivroDTO livro)
        {
            try
            {
                var input = await Task.Run(() => _repository.ObterLivro(livro.Id));

                if(input == null)
                {
                    return null;
                }

                input.DataAtualizacao = DateTime.Now;
                input.UsuarioAtualizacao = 1;

                var result = _repository.AtualizarLivro(input);

                if (result == null)
                {
                    return null;
                }

                return _mapper.mapper.Map<LivroDTO>(result);
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
                var livro = await Task.Run(() => _repository.ObterLivro(id));

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
