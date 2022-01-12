using MediatR;
using ProjectStoreBlazor.Server.Commands;
using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Threading;
using System.Threading.Tasks;
using ProjectStoreBlazor.Server.Queries;
using System.Collections.Generic;

namespace ProjectStoreBlazor.Server.Handlers
{
    public class GetProductListHandler : IRequestHandler<GetProductListQuery, List<ProductDto>>
    {
        private IProductService _service { get; }

        public GetProductListHandler(IProductService service)
        {
            _service = service;
        }

        
        public Task<List<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            return _service.ProductGet();
        }
    }
}
