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
    /// Tells users when someone views their profile, if this feature is available.
    /// </summary>
    public class ProfileViewNotificationEmailHandler : IRequestHandler<ProfileViewNotificationEmailRequest, ProfileViewNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ProfileViewNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ProfileViewNotificationEmailResponse> Handle(ProfileViewNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
