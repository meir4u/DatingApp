using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Application.DTOs.Photo.Validators
{
    public class PhotoForApprovalDtoValidator : AbstractValidator<PhotoForApprovalDto>
    {
        public PhotoForApprovalDtoValidator()
        {
            RuleFor(p => p.UserName)
                .NotNull().WithMessage("UserName is required.")
                .NotEmpty().WithMessage("UserName cannot be empty.");
        }
    }
}
