using MediatR;
using ProjectStoreBlazor.Server.Commands;
using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Server.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand,Unit>
    {
        private IProductService _service { get; }
        public DeleteProductHandler(IProductService service)
        {
            _service = service;
        }

        Task<Unit> IRequestHandler<DeleteProductCommand, Unit>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _service.ProductDelete(request.id, request.user);
            return Task.FromResult(Unit.Value);
        }
    }
}
