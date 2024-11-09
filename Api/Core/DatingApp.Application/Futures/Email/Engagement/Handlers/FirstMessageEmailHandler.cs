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
    ///  Congratulates users on their first message sent on the platform to encourage engagement.
    /// </summary>
    public class FirstMessageEmailHandler : IRequestHandler<FirstMessageEmailRequest, FirstMessageEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public FirstMessageEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<FirstMessageEmailResponse> Handle(FirstMessageEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
