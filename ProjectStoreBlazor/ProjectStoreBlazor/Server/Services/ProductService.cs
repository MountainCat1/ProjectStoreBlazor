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
            using var transaction = context.Database.BeginTransaction();

            try
            {
                List<Product> entities = await context.Products.ToListAsync();
                List<ProductDto> dtos = mapper.Map<List<ProductDto>>(entities);

                return dtos;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;// Task.CompletedTask;
            }
        }

        public async Task<ProductDto> ProductGet(int id)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                Product entity = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
                ProductDto dto = mapper.Map<ProductDto>(entity);

                return dto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;// Task.CompletedTask;
            }
        }

        public async Task ProductAdd(ProductDto productDto,int userId )
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                Product product = mapper.Map<Product>(productDto);
                product.CreatedByUserId = userId;
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                transaction.Commit();

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;// Task.CompletedTask;
            }
        }

        public async Task ProductDelete(int id, ClaimsPrincipal user)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                Product product = await context.Products.FindAsync(id);
                AuthorizationResult authorizationResult = await authorizationService.AuthorizeAsync(
                    user, product, new ResourceOperationRequirement(ResourceOperation.Delete)
                );

                if (!authorizationResult.Succeeded)
                {
                    throw new ForbidException();
                }
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                transaction.Commit();

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;// Task.CompletedTask;
            }
        }

        public async Task ProductUpdate(ProductDto productDto,ClaimsPrincipal user)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                Product product = mapper.Map<Product>(productDto);

                AuthorizationResult authorizationResult = await authorizationService.AuthorizeAsync(
                    user, product, new ResourceOperationRequirement(ResourceOperation.Update)
                );
                if (!authorizationResult.Succeeded) 
                {
                    throw new ForbidException();
                }

                context.Products.Update(product);
                await context.SaveChangesAsync();
                transaction.Commit();

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;// Task.CompletedTask;
            }
        }
    }
}
