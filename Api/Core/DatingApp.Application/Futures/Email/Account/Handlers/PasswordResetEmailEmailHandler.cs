using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Like.Requests;
using DatingApp.Application.Futures.Like.Responses;
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
    /// Allows users to reset their password if they forget it.
    /// </summary>
    public class PasswordResetEmailEmailHandler : IRequestHandler<Requests.PasswordResetEmailRequest, Responses.PasswordResetEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public PasswordResetEmailEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }

        public Task<Responses.PasswordResetEmailResponse> Handle(Requests.PasswordResetEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
