using DatingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Services
{
    public interface IEnhancedEmailService
    {
        Task ScheduleEmailAsync(string recipientEmail, string subject, string body, DateTime sendAt);
        Task ScheduleEmailAsync(string recipientEmail, string subject, string body, string attachmentPath, DateTime sendAt);
        Task ScheduleEmailAsync(TemplatedEmailJobData jobData, DateTime sendAt);
    }
}
