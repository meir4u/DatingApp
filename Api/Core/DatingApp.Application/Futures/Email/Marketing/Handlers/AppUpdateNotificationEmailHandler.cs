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
    /// Notifies users when a major app update has been released, highlighting improvements or new features.
    /// </summary>
    public class AppUpdateNotificationEmailHandler : IRequestHandler<AppUpdateNotificationEmailRequest, AppUpdateNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public AppUpdateNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<AppUpdateNotificationEmailResponse> Handle(AppUpdateNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
