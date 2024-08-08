using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Adapter.Google
{
    public interface IGoogleAuthAdapter
    {
        Task<string> GetGoogleTokenAsync(string code);
        Task<GoogleUserInfo> GetUserInfoAsync(string accessToken);
    }
}
