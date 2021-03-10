using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using API_Rest_GraphQl.Queries;
using API_Rest_GraphQl.Repositorios.Interfaces;

namespace API_Rest_GraphQl.Controllers.GraphQl
{
    [ApiController]
    [Route("api/graph/[controller]")]
    public class LivroGraphController : Controller
    {
        private readonly ILivroRepository _repository;
        public LivroGraphController(ILivroRepository repository)
        {
            _repository = repository;
        }

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

        public async Task<IActionResult> Livro(GraphQLRequest request)
        {
            try
            {
                LivroQuery query = new LivroQuery(_repository);

                ExecutionOptions options = new ExecutionOptions()
                {
                    Query = request.Query,
                    OperationName = request.OperationName
                };
                DocumentExecuter document = new DocumentExecuter();
                var result = await document.ExecuteAsync(options);

                if(result.Errors.Count > 0)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
