﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Photo.Validators
{
    public class DeletePhotoDtoValidation : AbstractValidator<DeletePhotoDto>
    {
        public DeletePhotoDtoValidation()
        {
            
        }
    }
}