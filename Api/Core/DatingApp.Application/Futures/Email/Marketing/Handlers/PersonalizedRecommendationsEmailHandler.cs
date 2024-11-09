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
    ///  Sends personalized suggestions for improving profile visibility or increasing engagement, based on the user's behavior.
    /// </summary>
    public class PersonalizedRecommendationsEmailHandler : IRequestHandler<PersonalizedRecommendationsEmailRequest, PersonalizedRecommendationsEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public PersonalizedRecommendationsEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<PersonalizedRecommendationsEmailResponse> Handle(PersonalizedRecommendationsEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
