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
            List<Product> entities = await context.Products.ToListAsync();
            if (entities is null)
            {
                throw new NotFoundException("Products not found");
            }
            List<ProductDto> dtos = mapper.Map<List<ProductDto>>(entities);

            return dtos;
           
        }

        public async Task<ProductDto> ProductGet(int id)
        {
            Product entity = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
            {
                throw new NotFoundException("Product not found");
            }
            ProductDto dto = mapper.Map<ProductDto>(entity);

            return dto;
        }

        public async Task ProductAdd(ProductDto productDto,int userId )
        {
             Product product = mapper.Map<Product>(productDto);
             product.CreatedByUserId = userId;
             await context.Products.AddAsync(product);
             context.SaveChanges();
                

             await Task.CompletedTask;
           
        }

        public async Task ProductDelete(int id, ClaimsPrincipal user)
        {
             Product product = context.Products.Find(id);
             AuthorizationResult authorizationResult = await authorizationService.AuthorizeAsync(
                 user, product, new ResourceOperationRequirement(ResourceOperation.Delete));

             if (!authorizationResult.Succeeded)
             {
                 throw new ForbidException("No access");
             }
             context.Products.Remove(product);
             context.SaveChanges();
             
             await Task.CompletedTask;
           
        }

        public async Task ProductUpdate(int ProductId, ProductDto productDto, ClaimsPrincipal user)
        {
             var productInDb = context
                 .Products
                 .FirstOrDefault(p => p.Id == ProductId);
             if (productInDb is null)
             {
                 throw new NotFoundException("Product not found");
             }

             
             var authorizationResult = authorizationService.AuthorizeAsync
                (user, productInDb, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

             if (!authorizationResult.Succeeded)
             {
                 throw new ForbidException("access denied");
             }
            if(productDto.Name != null)
                productInDb.Name = productDto.Name;
            if (productDto.Description != null)
                productInDb.Description = productDto.Description;
            if (productDto.Image != null)
                productInDb.Image = productDto.Image;
            if (productDto.Price != null)
                productInDb.Price = productDto.Price;

             context.Products.Update(productInDb);
             context.SaveChanges();
             
             await Task.CompletedTask;
           
        }
    }
}
