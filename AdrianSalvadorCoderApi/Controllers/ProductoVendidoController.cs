using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdrianSalvadorCoderApi.Repositories;
using AdrianSalvadorCoderApi.Models;
using System.Data.SqlClient;
using static AdrianSalvadorCoderApi.Controllers.ProductoVendidoController;

namespace AdrianSalvadorCoderApi.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class ProductoVendidoController : Controller
        {
             private ProductoVendidoRepositorio repositorio = new ProductoVendidoRepositorio();

            [HttpGet]
            public ActionResult<List<ProductoVendido>> Get()
            {
                try
                {
                    List<ProductoVendido> lista = repositorio.GetProductoVendido();
                    return Ok(lista);
                }
                catch (Exception ex)
                {

                    return Problem(ex.Message);
                }
            }

            private List<ProductoVendido> GetProductosVendidos()
            {
                throw new NotImplementedException();
            }
        }


    
}
