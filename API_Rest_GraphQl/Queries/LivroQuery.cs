using API_Rest_GraphQl.Models.GraphTypes;
using API_Rest_GraphQl.Repositorios.Interfaces;
using GraphQL.Types;

namespace API_Rest_GraphQl.Queries
{
    public class LivroQuery : ObjectGraphType
    {
        public LivroQuery(ILivroRepository repository)
        {
            Field<ListGraphType<LivroType>>(
                "livros",
                resolve: context => 
                    repository.ObterLivros()
            );
        }
    }
}
