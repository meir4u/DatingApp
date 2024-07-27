﻿using DatingApp.Application.DTOs.Message;
using DatingApp.Application.Futures.Message.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Message.Requests
{
    public class CreateMessageCommand : IRequest<CreateMessageResponse>
    {
        public CreateMessageDto CreateMessage { get; set; }
    }
}
