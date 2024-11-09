using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Like.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Email.Account.Requests
{
    public class PasswordResetEmailRequest : IRequest<PasswordResetEmailResponse>
    {
    }
}
