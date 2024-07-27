using DatingApp.Application.DTOs.Register;
using DatingApp.Application.Futures.Account.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Account.Requests
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public RegisterDto Register { get; set; }
    }
}
