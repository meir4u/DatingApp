﻿using DatingApp.Application.DTOs.Photo;
using DatingApp.Application.Futures.Photo.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Photo.Requests
{
    public class SetMainPhotoCommand : IRequest<SetMainPhotoResponse>
    {
        public SetMainPhotoDto SetMainPhoto { get; set; }
    }
}
