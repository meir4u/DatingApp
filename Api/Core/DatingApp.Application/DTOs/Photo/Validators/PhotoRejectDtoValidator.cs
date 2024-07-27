using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Photo.Validators
{
    public class PhotoRejectDtoValidator : AbstractValidator<PhotoRejectDto>
    {
        public PhotoRejectDtoValidator()
        {
            RuleFor(p => p.PhotoId)
                .GreaterThan(0).WithMessage("PhotoId must be greater than zero.");
        }
    }
}
