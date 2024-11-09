using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string body);
        Task SendEmaillWithAttachmentAsync(string recipientEmail, string subject, string body, string attachmentPath);
        Task SendTemplatedEmaillWithAttachmentAsync<TModel>(string recipientEmail, string subject, string attachmentPath, string templateName, TModel model);
        Task SendTemplatedEmailAsync<TModel>(string recipientEmail, string subject, string templateName, TModel model);
    }
}
