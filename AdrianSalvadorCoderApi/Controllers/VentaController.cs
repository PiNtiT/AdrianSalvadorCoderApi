using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdrianSalvadorCoderApi.Repositories;
using AdrianSalvadorCoderApi.Models;
using System.Data.SqlClient;
using static AdrianSalvadorCoderApi.Controllers.VentaController;
using System;

namespace AdrianSalvadorCoderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : Controller
    {
         private VentaRepositorio repositorio = new VentaRepositorio();

        [HttpGet]
        public ActionResult<List<Venta>> Get()
        {
            try
            {
                List<Venta> lista = repositorio.GetVenta();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        private List<Venta> GetVenta()
        {
            throw new NotImplementedException();
        }
        
        
         [HttpPost] // Agregar una venta
        public ActionResult Post([FromBody] Venta venta) 
        {
            try
            {
                repositorio.agregarVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
