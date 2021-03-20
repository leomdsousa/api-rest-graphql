using API_Rest_GraphQl.Models.Entities;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_Rest_GraphQl.Controllers.Rest
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public AutenticacaoController(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Autentica o usuário
        /// </summary>
        /// <param name="user"> Usuário a ser autenticado </param>
        /// <returns code="200" > Sucesso na altenticaçaõ, retorna o usuário e o token </returns>
        /// <returns code="400" > Erro na requisição enviada </returns>
        /// <returns code="500" > Erro interno </returns>
        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public ActionResult Token(Usuario user)
        {
            try
            {
                var usuario = _usuarioRepository.ObterUsuario(user.Login, user.Senha);

                if (usuario == null)
                {
                    return NotFound();
                }

                var token = _tokenService.GerarToken(usuario);

                if (string.IsNullOrEmpty(token))
                {
                    return NotFound();
                }

                return Ok(new 
                {
                    usuario = usuario.ParseUsuarioDTO(usuario),
                    token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
