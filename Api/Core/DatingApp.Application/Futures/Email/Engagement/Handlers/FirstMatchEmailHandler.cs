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
    ///  Congratulates users on their first match sent on the platform to encourage engagement.
    /// </summary>
    public class FirstMatchEmailHandler : IRequestHandler<FirstMatchEmailRequest, FirstMatchEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public FirstMatchEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<FirstMatchEmailResponse> Handle(FirstMatchEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
