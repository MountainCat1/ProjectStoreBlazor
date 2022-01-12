using Microsoft.AspNetCore.Authorization;
using ProjectStoreBlazor.Server.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Server.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Product>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Product product)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read)
            { 
                context.Succeed(requirement);
            }


            bool valid = isValid(context, product);
            if (valid==true) 
            {
                context.Succeed(requirement);
            } 
            return Task.CompletedTask;
        }

        private bool isValid(AuthorizationHandlerContext context, Product product)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userIdIsTheSame = product.CreatedByUserId == int.Parse(userId);
            var isAdmin = "Admin" == context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            var isValid = userIdIsTheSame || isAdmin;
            return isValid;
        }
    }
}
