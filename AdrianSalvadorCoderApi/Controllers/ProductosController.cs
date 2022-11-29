using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdrianSalvadorCoderApi.Repositories;
using AdrianSalvadorCoderApi.Models;
using System.Data.SqlClient;

namespace AdrianSalvadorCoderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : Controller
    {
        private ProductosController Controller  = new ProductosController();
        // GET: ProductosController
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                
                    return Ok("Mensaje");
                
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }

      












        /*
        // GET: ProductosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
