using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Email.Base;
using DatingApp.Application.Futures.Email.Engagement.Requests;
using DatingApp.Application.Futures.Email.Engagement.Responses;
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

namespace DatingApp.Application.Futures.Email.Engagement.Handlers
{
    /// <summary>
    /// Encourages users to log in if they haven’t been active for a certain period, potentially with suggestions on new features or updates.
    /// </summary>
    public class ActivityReminderEmailHandler : BaseEmailHandler<ActivityReminderEmailRequest, ActivityReminderEmailResponse>
    {
        protected override string _templateName { get; set; } = "ActivityReminder";
        public ActivityReminderEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService)
             : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<ActivityReminderEmailResponse> Handle(ActivityReminderEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new ActivityReminderEmailResponse();
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
