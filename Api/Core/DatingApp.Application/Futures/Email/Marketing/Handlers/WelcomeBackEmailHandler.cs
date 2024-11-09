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
    /// Encourages inactive users to return to the app by showcasing new features, potential matches, or recent activity.
    /// </summary>
    public class WelcomeBackEmailHandler : IRequestHandler<WelcomeBackEmailRequest, WelcomeBackEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public WelcomeBackEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<WelcomeBackEmailResponse> Handle(WelcomeBackEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
