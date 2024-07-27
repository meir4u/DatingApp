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
            RuleFor(p => p.RecipientUsername)
                .NotNull().WithMessage("Recipient username is required.")
                .NotEmpty().WithMessage("Recipient username cannot be empty.");

            RuleFor(p => p.Username)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username cannot be empty.");

            RuleFor(p => p.Content)
                .NotNull().WithMessage("Content is required.")
                .NotEmpty().WithMessage("Content cannot be empty.");
        }
    }
}
