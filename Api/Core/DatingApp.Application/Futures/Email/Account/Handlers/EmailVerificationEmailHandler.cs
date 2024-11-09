using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Account.Handlers
{
    /// <summary>
    /// Sent immediately after signup or email update, requiring the user to verify their email address.
    /// </summary>
    public class EmailVerificationEmailHandler : IRequestHandler<EmailVerificationEmailRequest, EmailVerificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public EmailVerificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<EmailVerificationEmailResponse> Handle(EmailVerificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
