using API_Rest_GraphQl.Models;
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

        [HttpGet]
        [Route("teste")]
        [AllowAnonymous]
        public ActionResult Teste()
        {
            try
            {
                return Ok(new { teste = "sucesso" });
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("obter/{id}")]
        [Authorize(Roles = "Administrador, Usuario")]
        public async Task<ActionResult<dynamic>> Obter(decimal id)
        {
            try
            {
                var result = _livroService.ObterLivro(id);

                if(result == null)
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

        [HttpPost]
        [Route("inlcuir")]
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
