using Microsoft.AspNetCore.Mvc;
using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Security.Claims;
using MediatR;
using ProjectStoreBlazor.Server.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectStoreBlazor.Server.Commands;

namespace ProjectStoreBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        // Product CRUD
        [HttpPost]
        public async Task<IActionResult> ProductAdd(ProductDto product)
        {
            var userId = int.Parse(User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);

            return Ok(await mediator.Send(new AddProductCommand(product, userId)));
        }
        [HttpGet]
        public async Task<IActionResult> ProductGet()
        {
            return Ok(await mediator.Send(new GetProductListQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ProductGetById([FromRoute] int id)
        {
            return Ok(await mediator.Send(new GetProductByIdQuery(id)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> ProductDelete([FromRoute] int id)
        {
            return Ok(await mediator.Send(new DeleteProductCommand(id,User)));
        }
        [HttpPut]
        public async Task<IActionResult> ProductUpdate([FromRoute]int productId,ProductDto product)
        {
            productId = product.Id;
            return Ok(await mediator.Send(new UpdateProductCommand( productId, product,User)));
        }
    }
}
