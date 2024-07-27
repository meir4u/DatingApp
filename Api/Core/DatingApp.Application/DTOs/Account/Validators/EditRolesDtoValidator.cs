using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Account.Validators
{
    public class EditRolesDtoValidator : AbstractValidator<EditRolesDto>
    {
        public EditRolesDtoValidator()
        {
            RuleFor(p => p.Username)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Roles)
                .NotNull()
                .NotEmpty();
        }
    }
}
