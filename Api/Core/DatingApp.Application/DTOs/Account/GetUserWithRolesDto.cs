using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Account
{
    public class GetUserWithRolesDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }    
    }
}
