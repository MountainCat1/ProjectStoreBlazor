using MediatR;
using ProjectStoreBlazor.Shared.Models;

namespace ProjectStoreBlazor.Server.Commands
{
    public record LoginUserCommand(LoginDto dto) :IRequest<string>;
    
}
