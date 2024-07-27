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
            RuleFor(p => p.Username)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username cannot be empty.");

            RuleFor(p => p.MessageId)
                .GreaterThan(0).WithMessage("MessageId must be greater than zero.");
        }
    }
}
