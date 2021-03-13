﻿using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Models.DTOs;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<LivroDTO> ObterLivro(decimal id)
        {
            try
            {
                var livro = _repository.ObterLivro(id);

                if(livro == null)
                {
                    return null;
                }

                LivroDTO retorno = new LivroDTO()
                {
                    Id = livro.Id,
                    Nome = livro.Nome,
                    Autor = livro.Autor,
                    Lido = livro.Lido
                };

                return retorno;
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
                var livros = _repository.ObterLivros();

                if(livros == null)
                {
                    return null;
                }

                var retorno = new List<LivroDTO>();

                foreach(var item in livros)
                {
                    retorno.Add(new LivroDTO()
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Autor = item.Autor,
                        Lido = item.Lido
                    });
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
                Livro input = new Livro()
                {
                    Id = livro.Id,
                    Nome = livro.Nome,
                    Autor = livro.Autor,
                    Lido = livro.Lido,
                    DataInclusao =  DateTime.Now,
                    UsuarioInclusao = 1
                };

                var result = _repository.AdicionarLivro(input);

                if(result == null)
                {
                    return null;
                }

                LivroDTO retorno = new LivroDTO()
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Autor = result.Autor,
                    Lido = result.Lido
                };

                return retorno;
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
                var input = _repository.ObterLivro(livro.Id);

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

                LivroDTO retorno = new LivroDTO()
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Autor = result.Autor,
                    Lido = result.Lido
                };

                return retorno;
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

        private LivroDTO ParseLivroDTo(Livro livro)
        {
            return new LivroDTO()
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Lido = livro.Lido,
            };
        }

        private Livro ParseLivro(LivroDTO livro)
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
