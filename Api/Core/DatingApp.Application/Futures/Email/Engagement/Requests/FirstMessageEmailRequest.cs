﻿using DatingApp.Application.Futures.Email.Base;
using DatingApp.Application.Futures.Email.Engagement.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Engagement.Requests
{
    public class FirstMessageEmailRequest : BaseEmailRequest, IRequest<FirstMessageEmailResponse>
    {
    }
}
