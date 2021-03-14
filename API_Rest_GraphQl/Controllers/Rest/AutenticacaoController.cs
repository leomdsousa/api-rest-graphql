﻿using API_Rest_GraphQl.Models.Entities;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services;
using API_Rest_GraphQl.Services.Interfaces;
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
        private readonly ITokenService _tokenService;

        public AutenticacaoController(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

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
