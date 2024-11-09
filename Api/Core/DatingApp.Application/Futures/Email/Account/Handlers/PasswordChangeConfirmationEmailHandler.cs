using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Account.Handlers
{
    /// <summary>
    /// Notifies users that their password has been changed, including a way to report if the change wasn’t authorized.
    /// </summary>
    public class PasswordChangeConfirmationEmailHandler : IRequestHandler<PasswordChangeConfirmationEmailRequest, PasswordChangeConfirmationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public PasswordChangeConfirmationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<PasswordChangeConfirmationEmailResponse> Handle(PasswordChangeConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
