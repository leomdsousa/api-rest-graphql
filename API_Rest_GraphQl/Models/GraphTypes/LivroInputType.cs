using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace API_Rest_GraphQl.Models.GraphTypes
{
    public class LivroInputType : InputObjectGraphType
    {
        public LivroInputType()
        {
            //Name = "livro";

            Field<NonNullGraphType<StringGraphType>>("nome");
            Field<NonNullGraphType<StringGraphType>>("autor");
            Field<NonNullGraphType<BooleanGraphType>>("lido");
            Field<IntGraphType>("value");
        }
    }
}
