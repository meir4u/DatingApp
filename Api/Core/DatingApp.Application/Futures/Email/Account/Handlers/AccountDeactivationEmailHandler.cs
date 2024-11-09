using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Email.Account.Handlers
{
    /// <summary>
    ///  Confirms the user’s request to deactivate their account, including steps to reactivate if desired.
    /// </summary>
    public class AccountDeactivationEmailHandler : IRequestHandler<AccountDeactivationEmailRequest, AccountDeactivationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public AccountDeactivationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<AccountDeactivationEmailResponse> Handle(AccountDeactivationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
