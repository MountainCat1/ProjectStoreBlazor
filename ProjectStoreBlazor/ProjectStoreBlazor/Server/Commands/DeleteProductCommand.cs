using MediatR;
using System.Security.Claims;

namespace ProjectStoreBlazor.Server.Commands
{
    public record DeleteProductCommand(int id, ClaimsPrincipal user) :IRequest<Unit>;
    
    
}
