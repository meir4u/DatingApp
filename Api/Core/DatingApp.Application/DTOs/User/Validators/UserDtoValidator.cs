using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Application.DTOs.User.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            
        }
    }
}
