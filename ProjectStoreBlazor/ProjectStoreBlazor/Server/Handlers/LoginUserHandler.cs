using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Threading;
using System.Threading.Tasks;
using ProjectStoreBlazor.Server.Queries;
using System.Collections.Generic;
using ProjectStoreBlazor.Server.Commands;
using MediatR;

namespace ProjectStoreBlazor.Server.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
    {
        public IUserService _service { get; }
        public LoginUserHandler(IUserService service)
        {
            _service = service;
        }



        public Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GenerateJwt(request.dto));
        }
    }
}
