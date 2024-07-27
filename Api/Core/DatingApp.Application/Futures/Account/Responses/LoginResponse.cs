using DatingApp.Application.DTOs.User;
using DatingApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Account.Responses
{
    public class LoginResponse : BaseCommandResponse
    {
        public UserDto User { get; set; }
    }
}
