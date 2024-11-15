using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Email.Base
{
    public class BaseEmailRequest
    {
        public string RecipientEmail { get; set; }
        public string TemplateName { get; set; }
    }
}
