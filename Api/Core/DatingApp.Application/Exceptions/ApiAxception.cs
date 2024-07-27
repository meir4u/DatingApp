using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Application.Exceptions
{
    public class ApiAxception
    {
        public ApiAxception(int statusCode, string messages, string details)
        {
            StatusCode = statusCode;
            Messages = messages;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Messages { get; set; }
        public string Details { get; set; }
    }
}
