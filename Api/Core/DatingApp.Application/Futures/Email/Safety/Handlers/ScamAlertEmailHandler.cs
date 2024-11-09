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
    /// Warns users about potential scams or phishing attempts targeting dating app users.
    /// </summary>
    public class ScamAlertEmailHandler : IRequestHandler<ScamAlertEmailRequest, ScamAlertEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ScamAlertEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ScamAlertEmailResponse> Handle(ScamAlertEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
