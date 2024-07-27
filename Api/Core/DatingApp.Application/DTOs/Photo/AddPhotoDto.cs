using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Photo
{
    public class AddPhotoDto
    {
        public string Username { get; set; }
        public IFormFile File { get; set; }
    }
}
