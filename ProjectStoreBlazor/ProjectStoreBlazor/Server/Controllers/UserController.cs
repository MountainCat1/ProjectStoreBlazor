using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Threading.Tasks;
using ProjectStoreBlazor.Server.Commands;
using ProjectStoreBlazor.Server.Queries;
using System.Security.Claims;

namespace ProjectStoreBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserDto dto)
        {            
          return Ok(await _mediator.Send(new RegisterUserCommand(dto)));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            
            return Ok(await _mediator.Send(new LoginUserCommand(dto)));
        }
        [HttpGet]
        public async Task<IActionResult> GetUserDataFromSession()
        {
            var claim = User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                return NotFound(null);

            var userId = int.Parse(claim.Value);
            return Ok(await _mediator.Send(new GetUserDataQuery(userId)));
        }
    }

   
}
