using MediatR;
using ProjectStoreBlazor.Server.Queries;
using ProjectStoreBlazor.Server.Services;
using ProjectStoreBlazor.Shared.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Server.Handlers
{
    

        public class GetUserDataHandler : IRequestHandler<GetUserDataQuery, UserDto>
        {
            private IUserService _service { get; }

            public GetUserDataHandler(IUserService service)
            {
                _service = service;
            }


            public Task<UserDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_service.GetUserFromJWTToken(request.userId));
            }
        }
    
}
