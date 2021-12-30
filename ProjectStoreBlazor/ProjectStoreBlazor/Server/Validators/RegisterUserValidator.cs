using System.Linq;

using FluentValidation;

using ProjectStoreBlazor.Server.Entities;
using ProjectStoreBlazor.Shared.Models;


namespace ProjectStoreBlazor.Server.Validators
{
    public class RegisterUserValidator:AbstractValidator<RegisterUserDto>
    {
        

        public RegisterUserValidator(StoreDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse) 
                {
                    context.AddFailure("Email", "Email is taken");
                }
            });
            
        }

    }
}
