using DatingApp.Application.Futures.Email.Subscription.Requests;
using DatingApp.Application.Futures.Email.Subscription.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Subscription.Handlers
{
    /// <summary>
    /// Warns users before their free trial expires and suggests a paid plan if available.
    /// </summary>
    public class TrialExpiryNotificationEmailHandler : IRequestHandler<TrialExpiryNotificationEmailRequest, TrialExpiryNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public TrialExpiryNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<TrialExpiryNotificationEmailResponse> Handle(TrialExpiryNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
