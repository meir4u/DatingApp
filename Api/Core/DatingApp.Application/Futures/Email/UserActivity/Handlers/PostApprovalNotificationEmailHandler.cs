using DatingApp.Application.Futures.Email.UserActivity.Requests;
using DatingApp.Application.Futures.Email.UserActivity.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.UserActivity.Handlers
{
    /// <summary>
    /// Notifies users when their submitted content (e.g., profile picture or bio update) has been approved.
    /// </summary>
    public class PostApprovalNotificationEmailHandler : IRequestHandler<PostApprovalNotificationEmailRequest, PostApprovalNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public PostApprovalNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<PostApprovalNotificationEmailResponse> Handle(PostApprovalNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
