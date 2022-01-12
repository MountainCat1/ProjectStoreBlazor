using MediatR;
using ProjectStoreBlazor.Server.Commands;
using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Server.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Unit>
    {
        private IProductService _service { get; }
        public AddProductHandler(IProductService service)
        {
            _service = service;
        }

        Task<Unit> IRequestHandler<AddProductCommand, Unit>.Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            _service.ProductAdd(request.productDto, request.userId);
            return Task.FromResult(Unit.Value);
        }
    }
}
