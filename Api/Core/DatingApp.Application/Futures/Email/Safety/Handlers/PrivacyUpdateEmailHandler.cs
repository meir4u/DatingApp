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
    /// Informs users of changes to the privacy policy or terms of service, especially if data use is affected.
    /// </summary>
    public class PrivacyUpdateEmailHandler : IRequestHandler<PrivacyUpdateEmailRequest, PrivacyUpdateEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public PrivacyUpdateEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<PrivacyUpdateEmailResponse> Handle(PrivacyUpdateEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
