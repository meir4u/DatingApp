//using System.ComponentModel.DataAnnotations;

using System;

namespace DatingApp.Application.DTOs.Register
{
    public class RegisterDto
    {
        public string Username { get; set; }

        public string KnownAs { get; set; }

        public string Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; } //it is optional to make required work. DateOnly cann't be null by default.

        public string City { get; set; }

        public string Country { get; set; }

        public string Password { get; set; }
    }
}
