﻿using DatingApp.Application.DTOs.Photo;
using DatingApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Photo.Responses
{
    public class AddPhotoResponse : BaseCommandResponse
    {
        public string Username { get; set; }
        public PhotoDto Photo{ get; set; }
    }
}
