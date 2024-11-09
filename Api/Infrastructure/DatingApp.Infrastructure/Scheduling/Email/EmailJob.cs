using DatingApp.Domain.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Scheduling.Email
{
    internal class EmailJob : IJob
    {
        private readonly IEmailService _emailService;

        public EmailJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Retrieve job data
            var recipientEmail = context.MergedJobDataMap.GetString("recipientEmail");
            var subject = context.MergedJobDataMap.GetString("subject");
            var body = context.MergedJobDataMap.GetString("body");

            // Send the email using the injected email service
            await _emailService.SendEmailAsync(recipientEmail, subject, body);
        }
    }
}
