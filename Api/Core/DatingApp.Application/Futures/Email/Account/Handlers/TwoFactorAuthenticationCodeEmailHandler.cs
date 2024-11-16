using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Email.Base;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
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
    /// Sends a temporary code for users who have enabled two-factor authentication.
    /// </summary>
    public class TwoFactorAuthenticationCodeEmailHandler : BaseEmailHandler<TwoFactorAuthenticationCodeEmailRequest, TwoFactorAuthenticationCodeEmailResponse>
    {
        protected override string _templateName { get; set; } = "TwoFactorAuthenticationCode";
        public TwoFactorAuthenticationCodeEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService)
             : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<TwoFactorAuthenticationCodeEmailResponse> Handle(TwoFactorAuthenticationCodeEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new TwoFactorAuthenticationCodeEmailResponse();
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(request.Username);

                var emailJobData = new TemplatedEmailJobData()
                {
                    RecipientEmail = user.Email,
                    TemplateName = _templateName,
                };
                await _enhancedEmailService.ScheduleEmailAsync(emailJobData, DateTime.Now);
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
            }

            return response;
        }
    }
}
