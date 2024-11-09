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

namespace DatingApp.Application.Futures.Email.Account.Handlers
{
    /// <summary>
    /// Notifies users that their account has been successfully deleted and may include a reactivation link within a grace period.
    /// </summary>
    public class AccountDeletionConfirmationEmailHandler : IRequestHandler<AccountDeletionConfirmationEmailRequest, AccountDeletionConfirmationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public AccountDeletionConfirmationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<AccountDeletionConfirmationEmailResponse> Handle(AccountDeletionConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
