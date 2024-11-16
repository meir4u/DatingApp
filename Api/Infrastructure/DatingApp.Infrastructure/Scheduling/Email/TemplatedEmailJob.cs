using DatingApp.Domain.Entities;
using DatingApp.Domain.Services;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Scheduling.Email
{
    public class TemplatedEmailJob : IJob
    {
        private readonly IEmailService _emailService;

        public TemplatedEmailJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Retrieve the serialized job data from JobDataMap
            string jobDataJson = context.MergedJobDataMap.GetString("jobData")?.Trim() ?? string.Empty;

            if(string.IsNullOrEmpty(jobDataJson))
            {
                throw new ArgumentNullException("jobData", "Job data cannot be null or empty");
            }

            // Deserialize the job data back to TemplatedEmailJobData
            var jobData = JsonConvert.DeserializeObject<TemplatedEmailJobData>(jobDataJson);

            if(string.IsNullOrEmpty(jobData?.RecipientEmail))
            {
                throw new ArgumentNullException(nameof(jobData.RecipientEmail), $"{nameof(jobData.RecipientEmail)} email cannot be null or empty");
            }
            // Dynamically determine the model type
            var modelType = Type.GetType(jobData.ModelType);
            var model = JsonConvert.DeserializeObject(jobData.Model.ToString(), modelType);

            if (string.IsNullOrEmpty(jobData.AttachmentPath))
            {
                // Send the templated email with attachment using the deserialized data
                await _emailService.SendTemplatedEmaillWithAttachmentAsync(
                    jobData.RecipientEmail,
                    jobData.Subject,
                    jobData.AttachmentPath,
                    jobData.TemplateName,
                    model                    
                );
            }
            else
            {
                // Send the templated email with attachment using the deserialized data
                await _emailService.SendTemplatedEmailAsync(
                    jobData.RecipientEmail,
                    jobData.Subject,
                    jobData.TemplateName,
                    model
                );
            }
            
        }
    }
}
