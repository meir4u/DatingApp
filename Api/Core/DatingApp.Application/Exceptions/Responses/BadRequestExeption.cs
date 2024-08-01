using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Exceptions.Responses
{
    public class BadRequestExeption : BaseResponseExeption
    {
        public BadRequestExeption(string messages, string details = "") : base(statusCode: StatusCodes.Status400BadRequest, messages, details)
        {
        }
    }
}
