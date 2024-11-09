using DatingApp.Application.Futures.Email.Marketing.Requests;
using DatingApp.Application.Futures.Email.Marketing.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Marketing.Handlers
{
    /// <summary>
    /// Offers discounts or limited-time promotions around holidays or special occasions.
    /// </summary>
    public class SeasonalPromotionsEmailHandler : IRequestHandler<SeasonalPromotionsEmailRequest, SeasonalPromotionsEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SeasonalPromotionsEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SeasonalPromotionsEmailResponse> Handle(SeasonalPromotionsEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
