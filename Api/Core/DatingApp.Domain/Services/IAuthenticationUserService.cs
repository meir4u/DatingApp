using DatingApp.Domain.Adapter.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Services
{
    public interface IAuthenticationUserService
    {
        Task<GoogleUserInfo> AuthenticateWithGoogleAsync(string code);
    }
}
