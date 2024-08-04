using DatingApp.Application.DTOs.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Account.Requests
{
    public class UserLastActiveUpdateCommand : IRequest<Unit>
    {
        public UserLastActiveUpdateDto UserLastActiveUpdate { get; set; }
    }
}
