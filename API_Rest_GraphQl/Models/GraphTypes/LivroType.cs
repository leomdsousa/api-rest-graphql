using GraphQL.Types;

namespace API_Rest_GraphQl.Models.GraphTypes
{
    public class LivroType : ObjectGraphType<Livro> 
    {
        public LivroType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Name("id").Description("Identicador do livro");
            Field(x => x.Nome, type: typeof(StringGraphType)).Name("nome").Description("Nome do livro");
            Field(x => x.Autor, type: typeof(StringGraphType)).Name("autor").Description("Autor do livro");
            Field(x => x.Lido, type: typeof(BooleanGraphType)).Name("lido").Description("Condição de lido ou não lido do livro");
        }
    }
}
