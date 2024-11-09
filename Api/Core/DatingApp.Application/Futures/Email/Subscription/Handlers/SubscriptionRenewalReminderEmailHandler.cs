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
    /// Reminds users of an upcoming subscription renewal.
    /// </summary>
    public class SubscriptionRenewalReminderEmailHandler : IRequestHandler<SubscriptionRenewalReminderEmailRequest, SubscriptionRenewalReminderEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SubscriptionRenewalReminderEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SubscriptionRenewalReminderEmailResponse> Handle(SubscriptionRenewalReminderEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
