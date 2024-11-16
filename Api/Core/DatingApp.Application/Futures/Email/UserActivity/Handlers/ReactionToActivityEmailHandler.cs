﻿using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Application.Futures.Email.Base;
using DatingApp.Application.Futures.Email.UserActivity.Requests;
using DatingApp.Application.Futures.Email.UserActivity.Responses;
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

namespace DatingApp.Application.Futures.Email.UserActivity.Handlers
{
    /// <summary>
    /// Notifies users of reactions or interactions on user-generated content like stories or posts (if the app supports these).
    /// </summary>
    public class ReactionToActivityEmailHandler : BaseEmailHandler<ReactionToActivityEmailRequest, ReactionToActivityEmailResponse>
    {
        protected override string _templateName { get; set; } = "ReactionNotification";

        public ReactionToActivityEmailHandler(IUnitOfWork unitOfWork, IEnhancedEmailService enhancedEmailService)
             : base(unitOfWork, enhancedEmailService)
        {
        }
        public override async Task<ReactionToActivityEmailResponse> Handle(ReactionToActivityEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new ReactionToActivityEmailResponse();
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
