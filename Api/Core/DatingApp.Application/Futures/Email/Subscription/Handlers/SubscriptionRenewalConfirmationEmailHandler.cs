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
    public class SubscriptionRenewalConfirmationEmailHandler : IRequestHandler<SubscriptionRenewalConfirmationEmailRequest, SubscriptionRenewalConfirmationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SubscriptionRenewalConfirmationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SubscriptionRenewalConfirmationEmailResponse> Handle(SubscriptionRenewalConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
