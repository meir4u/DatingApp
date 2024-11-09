using DatingApp.Domain.Services;
using DatingApp.Infrastructure.Scheduling;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Infrastructure.Scheduling.Email;
using Newtonsoft.Json;
using DatingApp.Domain.Entities;

namespace DatingApp.Infrastructure.Email
{
    internal class EnhancedEmailService : IEnhancedEmailService
    {
        private readonly IScheduler _scheduler;

        public EnhancedEmailService(IEmailService emailService)
        {
            // Initialize Quartz scheduler
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = new EmailJobFactory(emailService);
            _scheduler.Start().Wait();
        }

        public async Task ScheduleEmailAsync(string recipientEmail, string subject, string body, DateTime sendAt)
        {
            // Define the job and add email details to JobDataMap
            var job = JobBuilder.Create<EmailJob>()
                .UsingJobData("recipientEmail", recipientEmail)
                .UsingJobData("subject", subject)
                .UsingJobData("body", body)
                .UsingJobData("attachmentPath", string.Empty)
                .Build();

            // Define the trigger to execute the job at the specified time
            var trigger = TriggerBuilder.Create()
                .StartAt(sendAt) // Schedule at the given time
                .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow()) // Fire immediately if missed
                .Build();

            // Schedule the job with the trigger
            await _scheduler.ScheduleJob(job, trigger);
        }

        public async Task ScheduleEmailAsync(string recipientEmail, string subject, string body, string attachmentPath, DateTime sendAt)
        {
            // Define the job and add email details to JobDataMap
            var job = JobBuilder.Create<EmailJob>()
                .UsingJobData("recipientEmail", recipientEmail)
                .UsingJobData("subject", subject)
                .UsingJobData("body", body)
                .UsingJobData("attachmentPath", attachmentPath)
                .Build();

            // Define the trigger to execute the job at the specified time
            var trigger = TriggerBuilder.Create()
                .StartAt(sendAt) // Schedule at the given time
                .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow()) // Fire immediately if missed
                .Build();

            // Schedule the job with the trigger
            await _scheduler.ScheduleJob(job, trigger);
        }

        public async Task ScheduleEmailAsync(TemplatedEmailJobData jobData, DateTime sendAt)
        {
            // Serialize the job data to JSON
            var jobDataJson = JsonConvert.SerializeObject(jobData);

            // Define the job and add the serialized job data to JobDataMap
            var job = JobBuilder.Create<TemplatedEmailJob>()
                .UsingJobData("jobData", jobDataJson) // Store serialized job data
                .Build();

            // Define the trigger to execute the job at the specified time
            var trigger = TriggerBuilder.Create()
                .StartAt(sendAt) // Schedule at the given time
                .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow()) // Fire immediately if missed
                .Build();

            // Schedule the job with the trigger
            await _scheduler.ScheduleJob(job, trigger);
        }
    }
}
