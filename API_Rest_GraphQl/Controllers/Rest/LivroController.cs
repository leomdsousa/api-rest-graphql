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
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize]
        public async Task<ActionResult<dynamic>> Get(decimal id)
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
        [Route("")]
        //[Authorize]
        public async Task<ActionResult<List<LivroDTO>>> GetTodos()
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
        [Route("")]
        //[Authorize]
        public async Task<ActionResult<dynamic>> Post(LivroDTO livro)
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
        [Route("")]
        //[Authorize]
        public async Task<ActionResult<dynamic>> Put(LivroDTO livro)
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
        [Route("{id}")]
        //[Authorize]
        public async Task<ActionResult<dynamic>> Delete(decimal id)
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
