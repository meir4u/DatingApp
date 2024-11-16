using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Email.Base;
using DatingApp.Application.Futures.Email.Marketing.Requests;
using DatingApp.Application.Futures.Email.Marketing.Responses;
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

namespace DatingApp.Application.Futures.Email.Marketing.Handlers
{
    /// <summary>
    ///  Sends personalized suggestions for improving profile visibility or increasing engagement, based on the user's behavior.
    /// </summary>
    public class PersonalizedRecommendationsEmailHandler : BaseEmailHandler<PersonalizedRecommendationsEmailRequest, PersonalizedRecommendationsEmailResponse>
    {
        protected override string _templateName { get; set; } = "PersonalizedRecommendations";

        public PersonalizedRecommendationsEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService)
             : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<PersonalizedRecommendationsEmailResponse> Handle(PersonalizedRecommendationsEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new PersonalizedRecommendationsEmailResponse();
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
