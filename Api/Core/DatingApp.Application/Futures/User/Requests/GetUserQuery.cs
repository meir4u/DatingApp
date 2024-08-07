﻿using DatingApp.Application.DTOs.User;
using DatingApp.Application.Futures.User.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.User.Requests
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public GetUserDto GetUser { get; set; }
    }
}
