using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.User.Validators
{
    public class GetUserDtoValidator : AbstractValidator<GetUserDto>
    {
        public GetUserDtoValidator()
        {
            RuleFor(p => p.CurrentUser)
                .NotNull().WithMessage("Current user is required.")
                .NotEmpty().WithMessage("Current user cannot be empty.");

            RuleFor(p => p.Username)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username cannot be empty.");
        }
    }
}
