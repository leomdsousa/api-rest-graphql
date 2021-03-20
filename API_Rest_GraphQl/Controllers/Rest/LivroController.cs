using API_Rest_GraphQl.Models.DTOs;
using API_Rest_GraphQl.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {

        private readonly ILivroService _livroService;
        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        /// <summary>
        /// Teste
        /// </summary>
        /// <returns code="200">Busca realizada com sucesso</returns>
        /// /// <returns code="400">Erro durante a requisição</returns>
        [HttpGet]
        [Route("teste")]
        [AllowAnonymous]
        public ActionResult Teste()
        {
            try
            {
                return Ok(new { teste = "sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtém livro pelo id informado
        /// </summary>
        /// <param name="id"> Identificação do livro para a filtragem </param>
        /// <returns code="200">Busca realizada com sucesso</returns>
        /// /// <returns code="400">Erro durante a requisição</returns>
        [HttpGet]
        [Route("obter/{id}")]
        [Authorize(Roles = "Administrador, Usuario")]
        public async Task<ActionResult<dynamic>> Obter(decimal id)
        {
            try
            {
                var result = _livroService.ObterLivro(id);

                if (result == null)
                {
                    return NotFound();
                }

                return await Task.FromResult(result.Result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtém todos os livros existentes na biblioteca
        /// </summary>
        /// <returns code="200">Busca realizada com sucesso</returns>
        /// /// <returns code="400">Erro durante a requisição</returns>
        [HttpGet]
        [Route("obter")]
        [Authorize(Roles = "Administrador, Usuario")]
        public async Task<ActionResult<List<LivroDTO>>> ObterTodos()
        {
            try
            {
                var result = _livroService.ObterLivros();

                if (result == null)
                {
                    return NotFound();
                }

                return await Task.FromResult(result.Result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Adiciona livro na biblioteca
        /// </summary>
        /// <param name="livro"> Livro a ser adicionado </param>
        /// <returns code="200">inclusão realizada com sucesso</returns>
        /// /// <returns code="400">Erro durante a requisição</returns>
        [HttpPost]
        [Route("adicionar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<dynamic>> Adicionar(LivroDTO livro)
        {
            try
            {
                var result = _livroService.AdicionarLivro(livro);

                if (result == null)
                {
                    return NotFound();
                }

                return await Task.FromResult(result.Result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Atualiza livro da biblioteca
        /// </summary>
        /// <param name="livro"> Livro a ser atualizado </param>
        /// <returns code="200">Atualização realizada com sucesso</returns>
        /// /// <returns code="400">Erro durante a requisição</returns>
        [HttpPut]
        [Route("atualizar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<dynamic>> Atualizar(LivroDTO livro)
        {
            try
            {
                var result = _livroService.AtualizarLivro(livro);

                if (result == null)
                {
                    return NotFound();
                }

                return await Task.FromResult(result.Result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Exclui livro da biblioteca
        /// </summary>
        /// <param name="id"> Identificação do livro a ser excluído </param>
        /// <returns code="200">Exclusão realizada com sucesso</returns>
        /// /// <returns code="400">Erro durante a requisição</returns>
        [HttpDelete]
        [Route("excluir/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<dynamic>> Excluir(decimal id)
        {
            try
            {
                var result = _livroService.ExcluirLivro(id);

                return await Task.FromResult(result.Result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
