using API_Rest_GraphQl.Utilities.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API_Rest_GraphQl.Models.DTOs
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public Role Role { get; set; }
        public string RoleDescricao { get { return Role.ToString(); } }
    }
}
