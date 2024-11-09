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
    /// Shares success stories from other users to inspire engagement and confidence in the platform.
    /// </summary>
    public class SuccessStoriesEmailHandler : IRequestHandler<SuccessStoriesEmailRequest, SuccessStoriesEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SuccessStoriesEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SuccessStoriesEmailResponse> Handle(SuccessStoriesEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
