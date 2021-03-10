using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<Livro>> Get(decimal id)
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
        public async Task<ActionResult<List<Livro>>> GetTodos()
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
        public async Task<ActionResult<Livro>> Post(Livro livro)
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
        public async Task<ActionResult<Livro>> Put(Livro livro)
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
        public async Task<ActionResult<bool>> Delete(decimal id)
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
