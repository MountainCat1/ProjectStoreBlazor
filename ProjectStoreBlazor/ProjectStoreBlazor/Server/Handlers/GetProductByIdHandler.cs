using MediatR;
using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Queries;
using ProjectStoreBlazor.Server.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace ProjectStoreBlazor.Server.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private IProductService _service { get; }

        public GetProductByIdHandler(IProductService service)
        {
            _service = service;
        }


        public Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return _service.ProductGet(request.id);
        }
    }
}
