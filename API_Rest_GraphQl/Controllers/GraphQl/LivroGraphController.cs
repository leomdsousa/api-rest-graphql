using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using API_Rest_GraphQl.Queries;
using API_Rest_GraphQl.Repositorios.Interfaces;
using GraphQL.Types;
using API_Rest_GraphQl.Utilities;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using API_Rest_GraphQl.Mutations;
using Newtonsoft.Json;

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
                throw ex;
            }
        }

        [HttpGet]
        [Route("livro")]
        [Produces("application/json")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> LivroQuery([FromBody] GraphQLQuery request)
        {
            try
            {
                if(request == null)
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
                    Inputs = inputs
                };
                
                var result = await new DocumentExecuter()
                            .ExecuteAsync(options)
                            .ConfigureAwait(false);

                if(result.Errors?.Count() > 0)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("livro")]
        [Produces("application/json")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> LivroMutation([FromBody] GraphQLQuery request)
        {
            try
            {
                if(request == null)
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
                    Inputs = JsonConvert.DeserializeObject<Inputs>(inputs.ToString())
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
            catch(Exception ex)
            {
                throw ex; 
            }
        }
    }
}
