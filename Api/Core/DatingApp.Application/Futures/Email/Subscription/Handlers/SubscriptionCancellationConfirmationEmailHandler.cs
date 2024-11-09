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
    /// Confirms that a user’s subscription has been canceled.
    /// </summary>
    public class SubscriptionCancellationConfirmationEmailHandler : IRequestHandler<SubscriptionCancellationConfirmationEmailRequest, SubscriptionCancellationConfirmationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SubscriptionCancellationConfirmationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SubscriptionCancellationConfirmationEmailResponse> Handle(SubscriptionCancellationConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
