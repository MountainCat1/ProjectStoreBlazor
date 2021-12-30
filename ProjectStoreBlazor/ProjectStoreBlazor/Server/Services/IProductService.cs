using ProjectStoreBlazor.Shared.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace ProjectStoreBlazor.Server.Services
{
    public interface IProductService
    {
        string ProductAdd(ProductDto product,int userId);
        string ProductDelete(int id, ClaimsPrincipal user);
        string ProductUpdate(ProductDto product, ClaimsPrincipal user);
        List<ProductDto> ProductGet();
    }
}