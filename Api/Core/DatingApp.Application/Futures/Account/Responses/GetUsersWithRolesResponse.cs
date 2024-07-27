using DatingApp.Application.DTOs.Account;
using DatingApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Account.Responses
{
    public class GetUsersWithRolesResponse : BaseCommandResponse
    {
        public IEnumerable<GetUserWithRolesDto> UserWithRoles { get; set; }
    }
}
