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
    /// Notifies users when someone likes their profile.
    /// </summary>
    public class NewLikeNotificationEmailHandler : IRequestHandler<NewLikeNotificationEmailRequest, NewLikeNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public NewLikeNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<NewLikeNotificationEmailResponse> Handle(NewLikeNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
