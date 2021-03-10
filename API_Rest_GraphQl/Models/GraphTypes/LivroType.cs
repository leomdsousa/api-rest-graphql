using GraphQL.Types;

namespace API_Rest_GraphQl.Models.GraphTypes
{
    public class LivroType : ObjectGraphType<Livro> 
    {
        public LivroType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Identicador do livro");
            Field(x => x.Nome, type: typeof(StringGraphType)).Description("Nome do livro");
            Field(x => x.Autor, type: typeof(StringGraphType)).Description("Autor do livro");
            Field(x => x.Lido, type: typeof(BooleanGraphType)).Description("Condição de lido ou não lido do livro");
        }
    }
}
