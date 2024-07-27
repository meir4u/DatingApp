using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Photo.Validators
{
    public class AddPhotoDtoValidator : AbstractValidator<AddPhotoDto>
    {
        public AddPhotoDtoValidator()
        {
            RuleFor(p => p.Username)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username cannot be empty.");

            RuleFor(p => p.File)
                .NotNull().WithMessage("File is required.")
                .Must(file => file.Length > 0).WithMessage("File cannot be empty.");
        }
    }
}
