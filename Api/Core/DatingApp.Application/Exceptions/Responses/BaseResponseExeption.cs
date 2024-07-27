using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Exceptions.Responses
{
    public abstract class BaseResponseExeption : Exception
    {
        public BaseResponseExeption(int statusCode, string messages, string details)
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
