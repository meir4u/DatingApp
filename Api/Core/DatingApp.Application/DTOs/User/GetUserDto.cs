using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.User
{
    public class GetUserDto
    {
        public string CurrentUser { get; set; }
        public string Username { get; set; }
    }
}
