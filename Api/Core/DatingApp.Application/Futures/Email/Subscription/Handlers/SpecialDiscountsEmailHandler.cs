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
    ///  Sends promotions for discounted subscriptions or upgrades, especially targeting inactive or free users.
    /// </summary>
    public class SpecialDiscountsEmailHandler : IRequestHandler<SpecialDiscountsEmailRequest, SpecialDiscountsEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SpecialDiscountsEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SpecialDiscountsEmailResponse> Handle(SpecialDiscountsEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
