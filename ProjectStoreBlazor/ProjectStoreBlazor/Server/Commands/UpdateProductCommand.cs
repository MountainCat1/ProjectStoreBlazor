using MediatR;
using ProjectStoreBlazor.Shared.Models;
using System.Security.Claims;

namespace ProjectStoreBlazor.Server.Commands
{
    public record UpdateProductCommand(int ProductId, ProductDto productDto, ClaimsPrincipal user):IRequest<Unit>;
    
    
}
