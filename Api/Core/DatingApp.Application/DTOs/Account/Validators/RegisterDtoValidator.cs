using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Application.DTOs.Register.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(p => p.Username)
                .NotNull()
                .NotEmpty()
                .Must(p=>p.Length > 3);

            RuleFor(p => p.KnownAs)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Gender)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.DateOfBirth)
                .NotNull().WithMessage("Date of Birth cannot be null.")
                .Must(date => date.HasValue && date.Value != DateOnly.MinValue).WithMessage("Date of Birth cannot be empty or default.");

            RuleFor(p => p.City)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Country)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Password)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password cannot be empty.")
                .Length(8, 50).WithMessage("Password must be between 8 and 50 characters.");

        }
    }
}
