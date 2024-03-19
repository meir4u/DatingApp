﻿using System.ComponentModel.DataAnnotations;

namespace DatingApp.Api.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateOnly? DateOfBirth { get; set; } //it is optional to make required work. DateOnly cann't be null by default.

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [StringLength(8, MinimumLength =4)]
        public string Password { get; set; }
    }
}
