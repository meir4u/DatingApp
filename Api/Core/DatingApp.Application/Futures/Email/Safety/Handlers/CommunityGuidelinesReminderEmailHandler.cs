using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Email.Base;
using DatingApp.Application.Futures.Email.Safety.Requests;
using DatingApp.Application.Futures.Email.Safety.Responses;
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

namespace DatingApp.Application.Futures.Email.Safety.Handlers
{
    /// <summary>
    /// Sends periodic reminders about community guidelines and expected behavior on the platform.
    /// </summary>
    public class CommunityGuidelinesReminderEmailHandler : BaseEmailHandler<CommunityGuidelinesReminderEmailRequest, CommunityGuidelinesReminderEmailResponse>
    {
        protected override string _templateName { get; set; } = "CommunityGuidelinesReminder";

        public CommunityGuidelinesReminderEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService)
             : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<CommunityGuidelinesReminderEmailResponse> Handle(CommunityGuidelinesReminderEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new CommunityGuidelinesReminderEmailResponse();
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
