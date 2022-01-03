using Microsoft.AspNetCore.Mvc;
using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace ProjectStoreBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        // Product CRUD
        [HttpGet]
        public async Task<IActionResult> ProductGet()
        {
            return Ok(await service.ProductGet());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ProductGet([FromRoute] int id)
        {
            return Ok(await service.ProductGet(id));
        }
        [HttpPost]
        public async Task<IActionResult> ProductAdd([FromForm] ProductDto product)
        {
            var userId = int.Parse(User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);

            Task task = service.ProductAdd(product, userId);

            task.Start();
            await task;

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> ProductDelete([FromRoute] int id)
        {
            Task task = service.ProductDelete(id, User);

            await task;
            task.Start();

            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> ProductUpdate([FromForm] ProductDto product)
        {
            Task task = service.ProductUpdate(product, User);

            await task;
            task.Start();

            return Ok();
        }
    }
}
