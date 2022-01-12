using MediatR;
using ProjectStoreBlazor.Shared.Models;

namespace ProjectStoreBlazor.Server.Queries
{
    public record GetProductByIdQuery(int id) : IRequest<ProductDto>;

}
