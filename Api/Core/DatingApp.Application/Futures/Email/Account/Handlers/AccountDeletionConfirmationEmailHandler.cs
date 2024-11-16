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

namespace DatingApp.Application.Futures.Email.Account.Handlers
{
    /// <summary>
    /// Notifies users that their account has been successfully deleted and may include a reactivation link within a grace period.
    /// </summary>
    public class AccountDeletionConfirmationEmailHandler : BaseEmailHandler<AccountDeletionConfirmationEmailRequest, AccountDeletionConfirmationEmailResponse>
    {
        protected override string _templateName { get; set; } = "AccountDeletionConfirmation";
        public AccountDeletionConfirmationEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService) : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<AccountDeletionConfirmationEmailResponse> Handle(AccountDeletionConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new AccountDeletionConfirmationEmailResponse();
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
