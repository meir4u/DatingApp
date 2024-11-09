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
    ///  Informs users if their account has been temporarily suspended due to a violation.
    /// </summary>
    public class AccountSuspensionNotificationEmailHandler : IRequestHandler<AccountSuspensionNotificationEmailRequest, AccountSuspensionNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public AccountSuspensionNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<AccountSuspensionNotificationEmailResponse> Handle(AccountSuspensionNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
