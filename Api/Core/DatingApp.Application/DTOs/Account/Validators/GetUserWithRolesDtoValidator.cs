﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Account.Validators
{
    public class GetUserWithRolesDtoValidator : AbstractValidator<GetUserWithRolesDto>
    {
        public GetUserWithRolesDtoValidator()
        {
            
        }
    }
}
