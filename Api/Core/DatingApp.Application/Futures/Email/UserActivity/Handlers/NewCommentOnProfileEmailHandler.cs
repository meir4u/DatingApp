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
    /// Notifies users when someone leaves a comment on their profile.
    /// </summary>
    public class NewCommentOnProfileEmailHandler : IRequestHandler<NewCommentOnProfileEmailRequest, NewCommentOnProfileEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public NewCommentOnProfileEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<NewCommentOnProfileEmailResponse> Handle(NewCommentOnProfileEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
