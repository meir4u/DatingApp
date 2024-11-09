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
    /// Encourages users to log in if they haven’t been active for a certain period, potentially with suggestions on new features or updates.
    /// </summary>
    public class ActivityReminderEmailHandler : IRequestHandler<ActivityReminderEmailRequest, ActivityReminderEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ActivityReminderEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ActivityReminderEmailResponse> Handle(ActivityReminderEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
