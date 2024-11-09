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
    /// Provides a curated list of potential matches based on the user’s preferences.
    /// </summary>
    public class MatchesEmailHandler : IRequestHandler<MatchesEmailRequest, MatchesEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public MatchesEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<MatchesEmailResponse> Handle(MatchesEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
