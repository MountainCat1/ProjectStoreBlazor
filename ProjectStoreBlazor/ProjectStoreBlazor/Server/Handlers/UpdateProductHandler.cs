using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Threading;
using System.Threading.Tasks;
using ProjectStoreBlazor.Server.Queries;
using System.Collections.Generic;
using ProjectStoreBlazor.Server.Commands;
using MediatR;

namespace ProjectStoreBlazor.Server.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        public IProductService _service { get; }
        public UpdateProductHandler(IProductService service)
        {
            _service = service;
        }

        public Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _service.ProductUpdate(request.ProductId, request.productDto, request.user);
            return Task.FromResult(Unit.Value);
        }
    }
}
