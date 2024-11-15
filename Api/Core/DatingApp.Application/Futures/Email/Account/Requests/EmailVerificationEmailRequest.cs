using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Email.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Email.Account.Requests
{
    public class EmailVerificationEmailRequest : BaseEmailRequest, IRequest<EmailVerificationEmailResponse>
    {
    }
}
