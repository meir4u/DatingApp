using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
    public class TemplatedEmailJobData

    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string AttachmentPath { get; set; }
        public string TemplateName { get; set; }
        public object Model { get; set; }
        public string ModelType { get; set; } // Stores the full type name as a string
    }
}
