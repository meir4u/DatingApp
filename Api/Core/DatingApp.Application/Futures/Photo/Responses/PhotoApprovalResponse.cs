using DatingApp.Application.DTOs.Photo;
using DatingApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Photo.Responses
{
    public class PhotoApprovalResponse : BaseCommandResponse
    {
        public PhotoForApprovalDto Photo { get; internal set; }
    }
}
