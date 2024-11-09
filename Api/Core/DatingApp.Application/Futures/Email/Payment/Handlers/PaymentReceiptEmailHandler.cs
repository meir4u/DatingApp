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
    /// Provides a receipt for any payments made on the platform, including in-app purchases or subscription fees.
    /// </summary>
    public class PaymentReceiptEmailHandler : IRequestHandler<PaymentReceiptEmailRequest, PaymentReceiptEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public PaymentReceiptEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<PaymentReceiptEmailResponse> Handle(PaymentReceiptEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
