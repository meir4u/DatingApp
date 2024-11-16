using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Email.Base;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
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
    public class AccountDeactivationEmailHandler : BaseEmailHandler<AccountDeactivationEmailRequest, AccountDeactivationEmailResponse>
    {
        protected override string _templateName { get; set; } = "AccountDeactivation";
        public AccountDeactivationEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService) 
            : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<AccountDeactivationEmailResponse> Handle(AccountDeactivationEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new AccountDeactivationEmailResponse();
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
