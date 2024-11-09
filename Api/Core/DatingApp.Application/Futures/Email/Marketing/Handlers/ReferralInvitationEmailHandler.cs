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
    /// Invites users to refer friends to the app, often with an incentive (like a free month of premium service).
    /// </summary>
    public class ReferralInvitationEmailHandler : IRequestHandler<ReferralInvitationEmailRequest, ReferralInvitationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ReferralInvitationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ReferralInvitationEmailResponse> Handle(ReferralInvitationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
