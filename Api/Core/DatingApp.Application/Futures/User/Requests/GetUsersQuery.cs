using DatingApp.Application.Futures.User.Responses;
using DatingApp.Application.Params;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.User.Requests
{
    public class GetUsersQuery : IRequest<GetUsersResponse>
    {
        public UserParams Params { get; set; }
    }
}
