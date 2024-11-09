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
    /// Alerts the user when someone they might be interested in is in their vicinity (if location-based features are enabled).
    /// </summary>
    public class UserNearbyNotificationEmailHandler : IRequestHandler<UserNearbyNotificationEmailRequest, UserNearbyNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public UserNearbyNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<UserNearbyNotificationEmailResponse> Handle(UserNearbyNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
