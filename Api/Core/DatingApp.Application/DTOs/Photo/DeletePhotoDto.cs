using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Photo
{
    public class DeletePhotoDto
    {
        public string Username { get; set; }
        public int PhotoId { get; set; }
    }
}
