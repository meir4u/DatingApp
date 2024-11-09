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
    /// Confirms that the user’s report (e.g., for inappropriate behavior or content) has been received and will be reviewed.
    /// </summary>
    public class ReportConfirmationEmailHandler : IRequestHandler<ReportConfirmationEmailRequest, ReportConfirmationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public ReportConfirmationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<ReportConfirmationEmailResponse> Handle(ReportConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
