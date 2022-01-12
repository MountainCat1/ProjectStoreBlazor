using MediatR;
using ProjectStoreBlazor.Shared.Models;

namespace ProjectStoreBlazor.Server.Commands
{
    public record AddProductCommand(ProductDto productDto, int userId) : IRequest<Unit>;
}
