using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdrianSalvadorCoderApi.Repositories;
using AdrianSalvadorCoderApi.Models;
using System.Data.SqlClient;
using static AdrianSalvadorCoderApi.Controllers.ProductosController;

namespace AdrianSalvadorCoderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : Controller
    {
       private ProductoRepositorio repositorio  = new ProductoRepositorio();
        // GET: ProductosController
        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto> lista = repositorio.GetProductos();
                return Ok(lista);
                
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }

        private List<Producto> GetProductos()
        {
            throw new NotImplementedException();
        }


        [HttpPost] //  Agregar un producto
        public ActionResult Post([FromBody] Producto producto) 
        {
            try
            {
                repositorio.crearProducto(producto);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete]
        public ActionResult Delete([FromBody] long id) //  Borrar un producto 
        {
            try
            {
                bool seElimino = repositorio.eliminarProducto(id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound(); 
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]  
        public ActionResult<Producto> Put(long id, [FromBody] Producto productoParaActualizar) //  Actualiza un producto 
        {
            try
            {
                Producto? productoActualizado = repositorio.actualizarProducto(id, productoParaActualizar);
                if (productoActualizado != null)
                {
                    return Ok(productoActualizado);
                }
                else
                {
                    return NotFound("El producto no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


       
    }
}
