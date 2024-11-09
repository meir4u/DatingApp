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
    /// Sends the results of any in-app polls the user participated in.
    /// </summary>
    public class PollResultsEmailHandler : IRequestHandler<PollResultsEmailRequest, PollResultsEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public PollResultsEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<PollResultsEmailResponse> Handle(PollResultsEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
