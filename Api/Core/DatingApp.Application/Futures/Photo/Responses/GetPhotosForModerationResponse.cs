using DatingApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Photo.Responses
{
    public class GetPhotosForModerationResponse : BaseCommandResponse
    {
        public IEnumerable<DatingApp.Domain.Entities.Photo> Photos { get; set; }
    }
}
