using Microsoft.AspNetCore.Mvc;
using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Security.Claims;

namespace ProjectStoreBlazor.Server.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }


        // Product CRUD
        [HttpPost]
        [Route("Product/Add")]
        public IActionResult ProductAdd(ProductDto product)
        {
            var userId = int.Parse(User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
            
            string message = service.ProductAdd(product,userId);
            return Created("", message);
        }
        [HttpGet]
        [Route("Product/Get")]
        public IActionResult ProductGet()
        {
            return Ok(service.ProductGet());
        }
        [HttpDelete]
        [Route("Product/Delete/{id}")]
        public IActionResult ProductDelete([FromRoute] int id)
        {
            string message = service.ProductDelete(id,User);
            return Ok(message);
        }
        [HttpPost]
        [Route("Product/Update")]
        public IActionResult ProductUpdate(ProductDto product)
        {
            string message = service.ProductUpdate(product,User);
            return Ok(message);
        }
    }
}
