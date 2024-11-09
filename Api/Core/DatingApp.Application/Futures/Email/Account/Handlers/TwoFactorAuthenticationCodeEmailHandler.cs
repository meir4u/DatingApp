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
    /// Sends a temporary code for users who have enabled two-factor authentication.
    /// </summary>
    public class TwoFactorAuthenticationCodeEmailHandler : IRequestHandler<TwoFactorAuthenticationCodeEmailRequest, TwoFactorAuthenticationCodeEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public TwoFactorAuthenticationCodeEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<TwoFactorAuthenticationCodeEmailResponse> Handle(TwoFactorAuthenticationCodeEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
