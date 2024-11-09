using DatingApp.Application.Futures.Email.Payment.Requests;
using DatingApp.Application.Futures.Email.Payment.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Payment.Handlers
{
    /// <summary>
    /// Notifies users when a payment fails, including steps to update payment information.
    /// </summary>
    public class FailedPaymentNotificationEmailHandler : IRequestHandler<FailedPaymentNotificationEmailRequest, FailedPaymentNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public FailedPaymentNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<FailedPaymentNotificationEmailResponse> Handle(FailedPaymentNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
