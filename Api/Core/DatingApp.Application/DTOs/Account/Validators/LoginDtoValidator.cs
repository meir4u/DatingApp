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

        }
    }
}
