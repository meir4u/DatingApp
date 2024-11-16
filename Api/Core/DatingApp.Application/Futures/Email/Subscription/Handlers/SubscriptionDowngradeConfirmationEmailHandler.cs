using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Email.Base;
using DatingApp.Application.Futures.Email.Subscription.Requests;
using DatingApp.Application.Futures.Email.Subscription.Responses;
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

namespace DatingApp.Application.Futures.Email.Subscription.Handlers
{
    /// <summary>
    /// Confirms changes to the user’s subscription plan.
    /// </summary>
    public class SubscriptionDowngradeConfirmationEmailHandler : BaseEmailHandler<SubscriptionDowngradeConfirmationEmailRequest, SubscriptionDowngradeConfirmationEmailResponse>
    {
        protected override string _templateName { get; set; } = "SubscriptionDowngradeConfirmation";

        public SubscriptionDowngradeConfirmationEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService)
             : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<SubscriptionDowngradeConfirmationEmailResponse> Handle(SubscriptionDowngradeConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new SubscriptionDowngradeConfirmationEmailResponse();
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
