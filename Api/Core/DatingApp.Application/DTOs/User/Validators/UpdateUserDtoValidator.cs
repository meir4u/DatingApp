using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.User.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(p => p.CurrentUser)
                .NotNull().WithMessage("Current user is required.")
                .NotEmpty().WithMessage("Current user cannot be empty.");
        }
    }
}
