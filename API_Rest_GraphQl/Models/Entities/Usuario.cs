using API_Rest_GraphQl.Models.DTOs;
using API_Rest_GraphQl.Utilities.Enums;

namespace API_Rest_GraphQl.Models.Entities
{
    public class Usuario : Comum
    {
        public decimal Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Role Role { get; set; }

        public UsuarioDTO ParseUsuarioDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                Nome = usuario.Nome,
                Login = usuario.Login,
                Role = usuario.Role,
            };
        }
    }
}
