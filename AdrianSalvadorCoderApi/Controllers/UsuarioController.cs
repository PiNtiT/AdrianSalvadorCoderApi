﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdrianSalvadorCoderApi.Repositories;
using AdrianSalvadorCoderApi.Models;
using System.Data.SqlClient;
using static AdrianSalvadorCoderApi.Controllers.UsuarioController;

namespace AdrianSalvadorCoderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioController Controller = new UsuarioController();

        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            try
            {
                List<Usuario> lista = GetUsuario();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        private List<Usuario> GetUsuario()
        {
            throw new NotImplementedException();
        }
    }
}