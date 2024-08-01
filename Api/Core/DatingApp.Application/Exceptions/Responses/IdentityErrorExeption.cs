using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Exceptions.Responses
{
    public class IdentityErrorExeption : BaseResponseExeption
    {
        public IEnumerable<IdentityError> Errors { get; set; }

        public IdentityErrorExeption(IEnumerable<IdentityError> errorList, string details = "") : base(statusCode: StatusCodes.Status401Unauthorized, errorList.FirstOrDefault()?.Description, details)
        {
            Errors = errorList;
        }
    }
}
