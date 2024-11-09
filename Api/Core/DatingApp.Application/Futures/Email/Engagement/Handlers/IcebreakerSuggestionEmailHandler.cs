using DatingApp.Application.Futures.Email.Engagement.Requests;
using DatingApp.Application.Futures.Email.Engagement.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Engagement.Handlers
{
    /// <summary>
    /// Provides users with conversation starters or tips for messaging their matches.
    /// </summary>
    public class IcebreakerSuggestionEmailHandler : IRequestHandler<IcebreakerSuggestionEmailRequest, IcebreakerSuggestionEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public IcebreakerSuggestionEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<IcebreakerSuggestionEmailResponse> Handle(IcebreakerSuggestionEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
