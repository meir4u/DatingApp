using DatingApp.Application.DTOs.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Account.Validators
{
    public class UserLastActiveUpdateDtoValidator : AbstractValidator<UserLastActiveUpdateDto>
    {
        public UserLastActiveUpdateDtoValidator()
        {
            RuleFor(p => p.UserId)
                .NotNull()
                .Must(p => p > 0);
            RuleFor(p => p.IsAuthenticated)
                .NotNull()
                .Must(p => p == true);
        }
    }
}
