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
    /// (for premium users): Provides insights on profile views, likes, and matches to encourage further interaction.
    /// </summary>
    public class ProfileAnalyticsEmailHandler : IRequestHandler<ProfileAnalyticsEmailRequest, ProfileAnalyticsEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ProfileAnalyticsEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ProfileAnalyticsEmailResponse> Handle(ProfileAnalyticsEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
