using DatingApp.Application.DTOs.Like;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Message.Validators
{
    public class DeleteMessageDtoValidator : AbstractValidator<DeleteMessageDto>
    {
        public DeleteMessageDtoValidator()
        {
            
        }
    }
}
