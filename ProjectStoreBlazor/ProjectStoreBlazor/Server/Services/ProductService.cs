using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectStoreBlazor.Server.Authorization;
using ProjectStoreBlazor.Server.Entities;
using ProjectStoreBlazor.Server.Exceptions;
using ProjectStoreBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly StoreDbContext context;
        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService;
        public ProductService(StoreDbContext context, IMapper mapper, IAuthorizationService authorizationService)
        {
            this.context = context;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }

        public async Task<List<ProductDto>> ProductGet()
        {
            

            try
            {
                List<Product> entities = await context.Products.ToListAsync();
                List<ProductDto> dtos = mapper.Map<List<ProductDto>>(entities);

                return dtos;
            }
            catch
            {
                
                throw;
            }
        }

        public async Task<ProductDto> ProductGet(int id)
        {
            

            try
            {
                Product entity = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
                ProductDto dto = mapper.Map<ProductDto>(entity);

                return dto;
            }
            catch(Exception e)
            {

                throw new Exception("Error while getting list of objects", e);
            }
        }

        public async Task ProductAdd(ProductDto productDto,int userId )
        {
            
            try
            {
                Product product = mapper.Map<Product>(productDto);
                product.CreatedByUserId = userId;
                await context.Products.AddAsync(product);
                context.SaveChanges();
                

                await Task.CompletedTask;
            }
            catch(Exception e)
            {
                
                throw new Exception("Error while adding an object",e);
            }
        }

        public async Task ProductDelete(int id, ClaimsPrincipal user)
        {
            

            try
            {
                Product product = context.Products.Find(id);
                AuthorizationResult authorizationResult = await authorizationService.AuthorizeAsync(
                    user, product, new ResourceOperationRequirement(ResourceOperation.Delete)
                );

                if (!authorizationResult.Succeeded)
                {
                    throw new ForbidException("");
                }
                context.Products.Remove(product);
                context.SaveChanges();
             
                await Task.CompletedTask;
            }
            catch(Exception e)
            {

                throw new Exception("Error while removing an object", e); ;
            }
        }

        public async Task ProductUpdate(int ProductId, ProductDto productDto, ClaimsPrincipal user)
        {
           

            try
            {
                var productInDb = context
                    .Products
                    .FirstOrDefault(p => p.Id == ProductId);
                if (productInDb is null)
                {
                    throw new NotFoundException("Product not found");
                }

                
                var authorizationResult = authorizationService.AuthorizeAsync(user, productInDb, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
                if (!authorizationResult.Succeeded)
                {
                    throw new ForbidException("access denied");
                }
                productInDb.Name = productDto.Name;
                productInDb.Description = productDto.Description;
                productInDb.Image = productDto.Image;
                productInDb.Price = productDto.Price;

                context.Products.Update(productInDb);
                context.SaveChanges();
                
                await Task.CompletedTask;
            }
            catch (Exception e)
            {

                throw new Exception("Error while adding an object", e);
            }
        }
    }
}
