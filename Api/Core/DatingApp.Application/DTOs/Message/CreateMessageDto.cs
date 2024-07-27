using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Application.DTOs.Message
{
    public class CreateMessageDto
    {
        public string RecipientUsername { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
