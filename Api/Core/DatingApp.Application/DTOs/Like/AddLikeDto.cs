using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Like
{
    public class AddLikeDto
    {
        public string Username { get; set; }
        public int SourceUserId { get; set; }
    }
}
