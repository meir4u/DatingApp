using DatingApp.Application.DTOs.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Application.DTOs.Account.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(p => p.Username)
                .NotNull()
                .NotEmpty()
                .Must(p=>p.Length > 3);

            RuleFor(p => p.Password)
                .NotNull()
                .NotEmpty()
                .Must(p => p.Length > 6);
        }
    }
}
