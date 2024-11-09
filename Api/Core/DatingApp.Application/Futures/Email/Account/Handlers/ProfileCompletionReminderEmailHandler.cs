using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Account.Handlers
{
    /// <summary>
    /// Encourages users to complete their profile if it's partially filled, with suggestions for fields to add.
    /// </summary>
    public class ProfileCompletionReminderEmailHandler : IRequestHandler<ProfileCompletionReminderEmailRequest, ProfileCompletionReminderEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ProfileCompletionReminderEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ProfileCompletionReminderEmailResponse> Handle(ProfileCompletionReminderEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
