using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ProjectStoreBlazor.Server.Authorization;
using ProjectStoreBlazor.Server.Entities;
using ProjectStoreBlazor.Server.Exceptions;
using ProjectStoreBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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

        public string ProductAdd(ProductDto productDto,int userId )
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                Product product = mapper.Map<Product>(productDto);
                product.CreatedByUserId = userId;
                context.Products.Add(product);
                context.SaveChanges();
                transaction.Commit();
                return $"Product {product.Name} added";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }

        public string ProductDelete(int id,ClaimsPrincipal user)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                Product product = context.Products.Find(id);
                var authorizationResult = authorizationService.AuthorizeAsync(user, product, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
                if (!authorizationResult.Succeeded)
                {
                    throw new ForbidException();
                }
                context.Products.Remove(product);
                context.SaveChanges();
                transaction.Commit();
                return $"Product {id} deleted";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }

        public List<ProductDto> ProductGet()
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                List<ProductDto> retList = mapper.Map<List<ProductDto>>(context.Products.ToList());

                return retList;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return null;
            }
        }

        public string ProductUpdate(ProductDto productDto,ClaimsPrincipal user)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                Product product = mapper.Map<Product>(productDto);
                var authorizationResult =authorizationService.AuthorizeAsync(user, product, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
                if (!authorizationResult.Succeeded) 
                {
                    throw new ForbidException();
                }
                context.Products.Update(product);
                context.SaveChanges();
                transaction.Commit();
                return $"Product {product.Name} updated";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }
    }
}
