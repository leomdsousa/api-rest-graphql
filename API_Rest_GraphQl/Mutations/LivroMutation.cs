using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Models.GraphTypes;
using API_Rest_GraphQl.Repositorios.Interfaces;
using GraphQL;
using GraphQL.Types;

namespace API_Rest_GraphQl.Mutations
{
    public class LivroMutation : ObjectGraphType
    {
        public LivroMutation(ILivroRepository _repository)
        {
            Field<LivroType>(
                "addLivro",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LivroInputType>>() { Name = "livro" }),
                resolve: context =>
                {
                    var livro = context.GetArgument<Livro>("livro");

                    if (livro == null)
                    {
                        return null;
                    }

                    return _repository.AdicionarLivro(livro);
                });
        }
    }
}
