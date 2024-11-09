using DatingApp.Application.Futures.Email.Safety.Requests;
using DatingApp.Application.Futures.Email.Safety.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Safety.Handlers
{
    /// <summary>
    /// Sends periodic reminders about community guidelines and expected behavior on the platform.
    /// </summary>
    public class CommunityGuidelinesReminderEmailHandler : IRequestHandler<CommunityGuidelinesReminderEmailRequest, CommunityGuidelinesReminderEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public CommunityGuidelinesReminderEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<CommunityGuidelinesReminderEmailResponse> Handle(CommunityGuidelinesReminderEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
