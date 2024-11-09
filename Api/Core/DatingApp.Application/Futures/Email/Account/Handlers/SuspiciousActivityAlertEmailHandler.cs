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
    /// Alerts the user if suspicious activity is detected, such as login from a new location or multiple failed login attempts.
    /// </summary>
    public class SuspiciousActivityAlertEmailHandler : IRequestHandler<SuspiciousActivityAlertEmailRequest, SuspiciousActivityAlertEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SuspiciousActivityAlertEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SuspiciousActivityAlertEmailResponse> Handle(SuspiciousActivityAlertEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
