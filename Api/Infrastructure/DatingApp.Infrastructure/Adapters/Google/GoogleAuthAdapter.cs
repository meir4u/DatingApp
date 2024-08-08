using DatingApp.Domain.Adapter.Google;
using DatingApp.Infrastructure.Adapters.Google.Model;
using DatingApp.Infrastructure.Params;
using Microsoft.Extensions.Configuration;
using Serilog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Common.Exceptions;

namespace DatingApp.Infrastructure.Adapters.Google
{
    public class GoogleAuthAdapter : IGoogleAuthAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleSettings _configuration;
        private readonly ILogger _logger;

        public GoogleAuthAdapter(HttpClient httpClient, GoogleSettings configuration, ILogger logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GetGoogleTokenAsync(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(_configuration.ClientId))
                    throw new ArgumentException("Google client ID is missing in the configuration.");
                if (string.IsNullOrEmpty(_configuration.ClientSecret))
                    throw new ArgumentException("Google client secret is missing in the configuration.");
                if (string.IsNullOrEmpty(_configuration.RedirectUri))
                    throw new ArgumentException("Google redirect URI is missing in the configuration.");

                var values = new Dictionary<string, string>
                {
                    { "code", code },
                    { "client_id", _configuration.ClientId },
                    { "client_secret", _configuration.ClientSecret },
                    { "redirect_uri", _configuration.RedirectUri },
                    { "grant_type", "authorization_code" }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<GoogleTokenResponse>(responseString);

                return tokenResponse.AccessToken;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Google Adapter Error: {Message}", ex.Message);
                throw new AdapterException(ex.Message, ex);
            }
            
        }

        public async Task<GoogleUserInfo> GetUserInfoAsync(string accessToken)
        {
            var response = await _httpClient.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={accessToken}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(responseString);

            return userInfo;
        }

        
    }
}
