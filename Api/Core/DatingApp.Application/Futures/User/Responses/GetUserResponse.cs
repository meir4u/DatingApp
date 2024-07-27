using DatingApp.Application.Responses;
using DatingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.User.Responses
{
    public class GetUserResponse : BaseCommandResponse
    {
        public AppUser User { get; internal set; }
    }
}
