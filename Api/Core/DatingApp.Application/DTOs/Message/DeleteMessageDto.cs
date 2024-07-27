using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Message
{
    public class DeleteMessageDto
    {
        public string Username { get; set; }
        public int MessageId { get; set; }
    }
}
