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
    /// Notifies users when their profile status changes (e.g., set to hidden or visible).
    /// </summary>
    public class ProfileVisibilityStatusEmailHandler : BaseEmailHandler<ProfileVisibilityStatusEmailRequest, ProfileVisibilityStatusEmailResponse>
    {
        protected override string _templateName { get; set; } = "ProfileVisibilityStatus";
        public ProfileVisibilityStatusEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService)
             : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<ProfileVisibilityStatusEmailResponse> Handle(ProfileVisibilityStatusEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new ProfileVisibilityStatusEmailResponse();
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
