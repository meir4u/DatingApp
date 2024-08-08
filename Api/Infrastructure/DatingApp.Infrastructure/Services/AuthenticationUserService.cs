using DatingApp.Domain.Adapter.Google;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Common.Exceptions;
using DatingApp.Domain.Services;

namespace DatingApp.Infrastructure.Services
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        private readonly IGoogleAuthAdapter _googleAuthAdapter;
        private readonly ILogger _logger;

        public AuthenticationUserService(IGoogleAuthAdapter googleAuthAdapter, ILogger logger)
        {
            _googleAuthAdapter = googleAuthAdapter;
            _logger = logger;
        }

        public async Task<GoogleUserInfo> AuthenticateWithGoogleAsync(string code)
        {
            try
            {
                var token = await _googleAuthAdapter.GetGoogleTokenAsync(code);
                var userInfo = await _googleAuthAdapter.GetUserInfoAsync(token);

                return userInfo;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Google Adapter Error: {Message}", ex.Message);
                throw new ServiceException(ex.Message, ex);
            }
            
            //// Generate JWT token
            //var claims = new[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id),
            //    new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //};

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var jwtToken = new JwtSecurityToken(
            //    _configuration["TokenIssuer"],
            //    _configuration["TokenIssuer"],
            //    claims,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: creds
            //);

            //return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
