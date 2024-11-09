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
    /// Confirms that the user has successfully subscribed to a premium plan.
    /// </summary>
    public class SubscriptionConfirmationEmailHandler : IRequestHandler<SubscriptionConfirmationEmailRequest, SubscriptionConfirmationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SubscriptionConfirmationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SubscriptionConfirmationEmailResponse> Handle(SubscriptionConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
