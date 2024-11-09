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
    /// Notifies users that their account has been permanently banned and provides details on appeal (if applicable).
    /// </summary>
    public class BanNotificationEmailHandler : IRequestHandler<BanNotificationEmailRequest, BanNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public BanNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<BanNotificationEmailResponse> Handle(BanNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
