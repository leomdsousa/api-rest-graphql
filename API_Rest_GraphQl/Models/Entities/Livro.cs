using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Models
{
    public class Livro
    {
        public decimal Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public bool Lido { get; set; }
    }
}
