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
    public class WelcomeEmailRequest : BaseEmailRequest, IRequest<WelcomeEmailResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VerificationUrl { get; set; }

    }
}
