using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Exceptions.Responses
{
    public class NotFoundException : BaseResponseExeption
    {
        public NotFoundException() : base(statusCode: StatusCodes.Status404NotFound, messages: string.Empty, details: string.Empty)
        {
        }
    }
}
