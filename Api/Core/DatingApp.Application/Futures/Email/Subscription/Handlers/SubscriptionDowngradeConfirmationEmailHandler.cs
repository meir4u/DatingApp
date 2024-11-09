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
    /// Confirms changes to the user’s subscription plan.
    /// </summary>
    public class SubscriptionDowngradeConfirmationEmailHandler : IRequestHandler<SubscriptionDowngradeConfirmationEmailRequest, SubscriptionDowngradeConfirmationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SubscriptionDowngradeConfirmationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SubscriptionDowngradeConfirmationEmailResponse> Handle(SubscriptionDowngradeConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
