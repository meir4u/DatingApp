﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.DTOs.Account
{
    public class UserLastActiveUpdateDto
    {
        public int UserId { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
