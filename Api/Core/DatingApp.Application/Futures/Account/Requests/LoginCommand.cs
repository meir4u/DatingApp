using DatingApp.Application.DTOs.Account;
using DatingApp.Application.Futures.Account.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Account.Requests
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginDto Login{ get; set; }
    }
}
