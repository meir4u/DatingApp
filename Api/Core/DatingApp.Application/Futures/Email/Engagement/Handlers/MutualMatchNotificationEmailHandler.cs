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
    ///  Notifies users when they have a new mutual match.
    /// </summary>
    public class MutualMatchNotificationEmailHandler : IRequestHandler<MutualMatchNotificationEmailRequest, MutualMatchNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public MutualMatchNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<MutualMatchNotificationEmailResponse> Handle(MutualMatchNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
