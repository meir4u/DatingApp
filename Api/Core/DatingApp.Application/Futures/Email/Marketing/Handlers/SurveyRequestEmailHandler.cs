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
    /// Requests feedback from users on the app experience, features, or specific updates.
    /// </summary>
    public class SurveyRequestEmailHandler : IRequestHandler<SurveyRequestEmailRequest, SurveyRequestEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SurveyRequestEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SurveyRequestEmailResponse> Handle(SurveyRequestEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
