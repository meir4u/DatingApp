using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Domain.Entities;
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
    ///  Sent when a user first signs up, welcoming them to the app, outlining key features, and encouraging them to complete their profile.
    /// </summary>
    public class WelcomeEmailEmailHandler : IRequestHandler<Requests.WelcomeEmailRequest, Responses.WelcomeEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public WelcomeEmailEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }

        public async Task<Responses.WelcomeEmailResponse> Handle(Requests.WelcomeEmailRequest request, CancellationToken cancellationToken)
        {
            var response = new Responses.WelcomeEmailResponse();
            try
            {
                var data = new TemplatedEmailJobData()
                {
                    RecipientEmail = request.RecipientEmail,
                    Subject = "Welcome",
                    AttachmentPath = string.Empty,
                    TemplateName = "WelcomeTemplate",
                    Model = request,
                    ModelType = typeof(Requests.WelcomeEmailRequest).AssemblyQualifiedName, // Store the full type name for later deserialization
                };
                await _enhancedEmailService.ScheduleEmailAsync(data, DateTime.Now);
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
            }

            return response;
        }
    }
}
