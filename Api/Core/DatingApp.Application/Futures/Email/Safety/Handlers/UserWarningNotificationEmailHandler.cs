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
    /// Notifies users if their account received a warning for behavior that violates community guidelines.
    /// </summary>
    public class UserWarningNotificationEmailHandler : IRequestHandler<UserWarningNotificationEmailRequest, UserWarningNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public UserWarningNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<UserWarningNotificationEmailResponse> Handle(UserWarningNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
