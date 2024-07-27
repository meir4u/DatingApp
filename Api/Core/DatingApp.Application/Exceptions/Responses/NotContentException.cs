using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Exceptions.Responses
{
    public class NotContentException : BaseResponseExeption
    {
        public NotContentException() : base(statusCode: StatusCodes.Status204NoContent, messages: string.Empty, details: string.Empty)
        {
        }
    }
}
