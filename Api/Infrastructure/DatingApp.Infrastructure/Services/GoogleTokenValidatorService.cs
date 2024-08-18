using DatingApp.Common.Exceptions;
using DatingApp.Domain.Services;
using DatingApp.Infrastructure.Params;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Services
{
    public class GoogleTokenValidatorService : IGoogleTokenValidatorService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<GoogleSettings> _configuration;
        private readonly ILogger _logger;

        public GoogleTokenValidatorService(HttpClient httpClient, IOptions<GoogleSettings> configuration, ILogger logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { _configuration.Value.ClientId } // Your Google client ID
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload;
            }
            catch (InvalidJwtException ex)
            {
                _logger.Error(ex, "Google JWT Validator Error: {Message}", ex.Message);
                //throw new ServiceException(ex.Message, ex);
                // Token validation failed
                return null;
            }
        }
    }
}
