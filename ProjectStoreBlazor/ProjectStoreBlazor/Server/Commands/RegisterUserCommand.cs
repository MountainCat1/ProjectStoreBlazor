using MediatR;
using ProjectStoreBlazor.Shared.Models;

namespace ProjectStoreBlazor.Server.Commands
{
    public record RegisterUserCommand(RegisterUserDto dto) :IRequest;
    
    
}
