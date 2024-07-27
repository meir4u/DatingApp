using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Exceptions.Responses
{
    public class NotAuthorizedException : BaseResponseExeption
    {
        public NotAuthorizedException(string messages) : base(statusCode: StatusCodes.Status404NotFound, messages: messages, details: string.Empty)
        {
        }
    }
}
