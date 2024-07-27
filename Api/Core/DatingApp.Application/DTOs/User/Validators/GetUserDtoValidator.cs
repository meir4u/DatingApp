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
            
        }
    }
}
