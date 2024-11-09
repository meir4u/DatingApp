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
    /// Invites users to virtual or in-person events organized by the dating app, such as speed dating, mixers, or webinars on dating tips.
    /// </summary>
    public class EventInvitationsEmailHandler : IRequestHandler<EventInvitationsEmailRequest, EventInvitationsEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public EventInvitationsEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<EventInvitationsEmailResponse> Handle(EventInvitationsEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
