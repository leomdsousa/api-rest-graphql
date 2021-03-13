using API_Rest_GraphQl.Models.Context;
using API_Rest_GraphQl.Models.Entities;
using API_Rest_GraphQl.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BibliotecaContext _context;

        public UsuarioRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public Usuario ObterUsuario(string login, string senha)
        {
            try
            {
                return _context.Usuarios
                    .Where(x => x.Login == login && x.Senha == senha)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
