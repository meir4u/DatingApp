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
    /// Alerts users when they receive a new message.
    /// </summary>
    public class NewMessageNotificationEmailHandler : IRequestHandler<NewMessageNotificationEmailRequest, NewMessageNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public NewMessageNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<NewMessageNotificationEmailResponse> Handle(NewMessageNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
