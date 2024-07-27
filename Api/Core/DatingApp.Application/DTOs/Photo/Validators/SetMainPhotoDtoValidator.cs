using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Photo.Validators
{
    public class SetMainPhotoDtoValidator : AbstractValidator<SetMainPhotoDto>
    {
        public SetMainPhotoDtoValidator()
        {
            RuleFor(p => p.Username)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username cannot be empty.");

            RuleFor(p => p.PhotoId)
                .GreaterThan(0).WithMessage("PhotoId must be greater than zero.");
        }
    }
}
