using API_Rest_GraphQl.Models.Entities;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace API_Rest_GraphQl.Controllers.Rest
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacaoController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public ActionResult Token(Usuario user)
        {
            dynamic result = new ExpandoObject();
            result.user = user;
            result.token = string.Empty;

            try
            {
                var usuario = _usuarioRepository.ObterUsuario(user.Login, user.Senha);

                if (usuario == null)
                {
                    return NotFound(result);
                }

                var token = TokenService.GerarToken(usuario);

                if (string.IsNullOrEmpty(token))
                {
                    return NotFound(result);
                }

                result.token = token;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
