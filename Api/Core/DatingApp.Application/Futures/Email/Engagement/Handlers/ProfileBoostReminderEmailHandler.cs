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
    /// Encourages users to boost their profile visibility to reach more potential matches.
    /// </summary>
    public class ProfileBoostReminderEmailHandler : IRequestHandler<ProfileBoostReminderEmailRequest, ProfileBoostReminderEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ProfileBoostReminderEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ProfileBoostReminderEmailResponse> Handle(ProfileBoostReminderEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
