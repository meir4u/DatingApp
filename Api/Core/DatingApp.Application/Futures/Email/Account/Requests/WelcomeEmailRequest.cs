using DatingApp.Application.Futures.Email.Account.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Email.Account.Requests
{
    public class WelcomeEmailRequest : IRequest<WelcomeEmailResponse>
    {
        public string RecipientEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VerificationUrl { get; set; }

    }
}
