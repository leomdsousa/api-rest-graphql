using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GraphQL;
using GraphQL.Types;
using API_Rest_GraphQl.Queries;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Utilities;
using API_Rest_GraphQl.Mutations;
using AllowAnonymousAttribute = GraphQL.AllowAnonymousAttribute;
using AuthorizeAttribute = GraphQL.AuthorizeAttribute;

namespace API_Rest_GraphQl.Controllers.GraphQL
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroGraphController : Controller
    {
        private readonly ILivroRepository _repository;
        public LivroGraphController(ILivroRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Teste
        /// </summary>
        /// <returns code="200"> Sucesso </returns>
        /// /// <returns code="400"> Erro </returns>
        [HttpGet]
        [Route("teste")]
        [AllowAnonymous]
        public IActionResult Teste()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Endpoint para obter livro ou livros 
        /// </summary>
        /// <param name="request"> Objeto contendo a query, o nome da operação e variáveis </param>
        /// <returns code="200"> Retorna a consulta solicitada </returns>
        /// <returns code="400"> Erro </returns>
        [HttpGet]
        [Route("livro")]
        [Produces("application/json")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> LivroQuery([FromBody] GraphQLQuery request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                dynamic inputs = request.Variables;

                ExecutionOptions options = new ExecutionOptions()
                {
                    Schema = new Schema()
                    {
                        Query = new LivroQuery(_repository)
                    },
                    Query = request.Query,
                    Variables = inputs
                    //Inputs = inputs
                };

                var result = await new DocumentExecuter()
                            .ExecuteAsync(options)
                            .ConfigureAwait(false);

                if (result.Errors?.Count() > 0)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Endpoint para inclusão ou alteração de livro
        /// </summary>
        /// <param name="request"> Objeto contendo a query, o nome da operação e variáveis </param>
        /// <returns code="200"> Retorna identificação do livro incluído ou alterado </returns>
        /// <returns code="400"> Erro </returns>
        [HttpPost]
        [Route("livro")]
        [Produces("application/json")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> LivroMutation([FromBody] GraphQLQuery request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                dynamic inputs = request.Variables;

                ExecutionOptions options = new ExecutionOptions()
                {
                    Schema = new Schema()
                    {
                        Mutation = new LivroMutation(_repository)
                    },
                    Query = request.Query,
                    Variables = JsonConvert.DeserializeObject<Inputs>(inputs.ToString())
                    //Inputs = JsonConvert.DeserializeObject<Inputs>(inputs.ToString())
                };

                var result = await new DocumentExecuter()
                    .ExecuteAsync(options)
                    .ConfigureAwait(false);

                if (result.Errors?.Count() > 0)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
