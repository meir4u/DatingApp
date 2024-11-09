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
    /// Notifies users of reactions or interactions on user-generated content like stories or posts (if the app supports these).
    /// </summary>
    public class ReactionToActivityEmailHandler : IRequestHandler<ReactionToActivityEmailRequest, ReactionToActivityEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ReactionToActivityEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ReactionToActivityEmailResponse> Handle(ReactionToActivityEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
