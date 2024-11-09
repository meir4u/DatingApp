using DatingApp.Domain.Services;
using Quartz.Spi;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Scheduling.Email
{
    internal class EmailJobFactory : IJobFactory
    {
        private readonly IEmailService _emailService;

        public EmailJobFactory(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new EmailJob(_emailService);
        }

        public void ReturnJob(IJob job) { /* No-op */ }
    }
}
