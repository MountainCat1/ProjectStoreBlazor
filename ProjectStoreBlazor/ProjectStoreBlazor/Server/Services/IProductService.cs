using ProjectStoreBlazor.Shared.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Server.Services
{
    public interface IProductService
    {
        Task ProductAdd(ProductDto product,int userId);
        Task ProductDelete(int id, ClaimsPrincipal user);
        Task ProductUpdate(ProductDto product, ClaimsPrincipal user);
        Task<List<ProductDto>> ProductGet();
        Task<ProductDto> ProductGet(int id);
    }
}