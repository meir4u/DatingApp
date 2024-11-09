using DatingApp.Application.Futures.Email.Safety.Requests;
using DatingApp.Application.Futures.Email.Safety.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Safety.Handlers
{
    /// <summary>
    /// Provides general safety tips, such as suggestions for safe online interactions, meeting in public places, or protecting personal information.
    /// </summary>
    public class SafetyTipsForDatingEmailHandler : IRequestHandler<SafetyTipsForDatingEmailRequest, SafetyTipsForDatingEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public SafetyTipsForDatingEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<SafetyTipsForDatingEmailResponse> Handle(SafetyTipsForDatingEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
