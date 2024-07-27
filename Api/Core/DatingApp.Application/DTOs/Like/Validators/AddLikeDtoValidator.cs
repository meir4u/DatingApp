using DatingApp.Application.DTOs.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Like.Validators
{
    public class AddLikeDtoValidator : AbstractValidator<AddLikeDto>
    {
        public AddLikeDtoValidator()
        {
            RuleFor(p => p.Username)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username cannot be empty.");

            RuleFor(p => p.SourceUserId)
                .NotNull().WithMessage("SourceUserId is required.")
                .GreaterThan(0).WithMessage("SourceUserId must be greater than zero.");
        }
    }
}
