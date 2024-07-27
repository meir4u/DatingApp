using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Application.DTOs.Message.Validators
{
    public class CreateMessageDtoValidator : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageDtoValidator()
        {
            
        }
    }
}
