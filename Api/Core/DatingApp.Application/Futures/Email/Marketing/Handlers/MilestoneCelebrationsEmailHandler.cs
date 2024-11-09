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
    /// Celebrates milestones such as the app’s anniversary, the user's anniversary on the platform, or reaching a certain number of matches/messages.
    /// </summary>
    public class MilestoneCelebrationsEmailHandler : IRequestHandler<MilestoneCelebrationsEmailRequest, MilestoneCelebrationsEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public MilestoneCelebrationsEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<MilestoneCelebrationsEmailResponse> Handle(MilestoneCelebrationsEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
