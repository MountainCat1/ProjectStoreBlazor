using MediatR;
using ProjectStoreBlazor.Shared.Models;

namespace ProjectStoreBlazor.Server.Queries
{
    public record GetUserDataQuery(int userId) :IRequest<UserDto>;
   
}
