﻿using API_Rest_GraphQl.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Repositorios.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuario(string login, string senha);
    }
}
