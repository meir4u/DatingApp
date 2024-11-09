using DatingApp.Domain.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Infrastructure.Params;
using System.Net.Http;
using DatingApp.Infrastructure.Scheduling;
using Quartz;
using FluentEmail.Core;
using RazorLight;

namespace DatingApp.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IFluentEmail _email;

        public EmailService(
            IOptions<EmailSettings> emailSettings,
            IFluentEmail email)
        {
            _emailSettings = emailSettings.Value;
            _email = email;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var res = await _email
                .To(recipientEmail)
                .Subject(subject)
                .Body(body, isHtml: true)
                .SendAsync();
        }

        public async Task SendEmaillWithAttachmentAsync(string recipientEmail, string subject, string body, string attachmentPath)
        {
            await _email
                .To(recipientEmail)
                .Subject(subject)
                .Body(body, isHtml: true)
                .AttachFromFilename(attachmentPath)
                .SendAsync();
        }

        public async Task SendTemplatedEmaillWithAttachmentAsync<TModel>(string recipientEmail, string subject, string attachmentPath, string templateName, TModel model)
        {
            await _email
                .To(recipientEmail)
                .Subject(subject)
                .AttachFromFilename(attachmentPath)
                .UsingTemplateFromFile($"Email\\Templates\\En\\{templateName}.cshtml", model) // Specify template path
                .SendAsync();
        }

        public async Task SendTemplatedEmailAsync<TModel>(string recipientEmail, string subject, string templateName, TModel model)
        {
            //Email\Templates\En\WelcomeTemplate
            await _email
                .To(recipientEmail)
                .Subject(subject)
                .UsingTemplateFromFile($"Email\\Templates\\En\\{templateName}.cshtml", model) // Specify template path
                .SendAsync();
        }

    }
}
