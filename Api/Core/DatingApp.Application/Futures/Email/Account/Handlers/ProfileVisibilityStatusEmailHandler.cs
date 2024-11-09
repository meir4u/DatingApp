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
    /// Notifies users when their profile status changes (e.g., set to hidden or visible).
    /// </summary>
    public class ProfileVisibilityStatusEmailHandler : IRequestHandler<ProfileVisibilityStatusEmailRequest, ProfileVisibilityStatusEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ProfileVisibilityStatusEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ProfileVisibilityStatusEmailResponse> Handle(ProfileVisibilityStatusEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
