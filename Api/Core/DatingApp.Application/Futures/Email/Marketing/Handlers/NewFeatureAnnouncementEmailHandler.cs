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
    /// Introduces new features on the platform, encouraging users to try them out.
    /// </summary>
    public class NewFeatureAnnouncementEmailHandler : IRequestHandler<NewFeatureAnnouncementEmailRequest, NewFeatureAnnouncementEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public NewFeatureAnnouncementEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<NewFeatureAnnouncementEmailResponse> Handle(NewFeatureAnnouncementEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
