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
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(32);

            RuleFor(x => x.Surname)
                .MinimumLength(1)
                .MaximumLength(32);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(32);
            
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(32);

            RuleFor(x => x.PhoneNumber)
                .Length(9);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
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
