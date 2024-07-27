using DatingApp.Application.DTOs.Message;
using DatingApp.Application.Futures.Message.Responses;
using DatingApp.Application.Params;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Message.Requests
{
    public class GetMessagesForUserQuery : IRequest<GetMessagesForUserResponse>
    {
        public MessageParams Params { get; set; }
    }
}
