using ProjectStoreBlazor.Shared.Models;
using ProjectStoreBlazor.Server.Services;
using System.Threading;
using System.Threading.Tasks;
using ProjectStoreBlazor.Server.Commands;
using MediatR;

namespace ProjectStoreBlazor.Server.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand,Unit>
    {
        public IUserService _service { get; }
        public RegisterUserHandler(IUserService service)
        {
            _service = service;
        }

        public Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _service.RegisterUser(request.dto);
            return Task.FromResult(Unit.Value);
        }
    }
}
