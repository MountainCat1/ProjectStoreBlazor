using MediatR;
using ProjectStoreBlazor.Server.Entities;
using ProjectStoreBlazor.Shared.Models;
using System.Collections.Generic;

namespace ProjectStoreBlazor.Server.Queries
{
    public record GetProductListQuery() :IRequest<List<ProductDto>>;
    
   
    
}
