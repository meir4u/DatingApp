using DatingApp.Domain.Entities;
using DatingApp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingApp.Api.Services
{
    // Defines a service responsible for token generation operations.
    public class TokenService : ITokenService
    {
        // Holds the symmetric security key used for token signing.
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
        private readonly UserManager<AppUser> _userManager;

        // Constructor that initializes the TokenService with configuration settings.
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            var tokenKey = config["TokenKey"];
            // Retrieves the token key from configuration and creates a symmetric security key.
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            _userManager = userManager;
        }

        // Creates a JWT token for a given user.
        public async Task<string> CreateToken(AppUser user)
        {
            // Define the claims to be included in the token. In this case, the user's name.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Creates signing credentials using the symmetric security key and HMAC SHA512 as the algorithm.
            var creds = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            // Prepares the token descriptor with claims, expiry date, and signing credentials.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // The claims identity for the token.
                Expires = DateTime.UtcNow.AddDays(7), // Sets the token to expire in 7 days.
                SigningCredentials = creds, // The signing credentials for the token.
                
            };

            // Instantiates the token handler to create and write the token.
            var tokenHandler = new JwtSecurityTokenHandler();

            // Creates the token based on the descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serializes the token to a string and returns it.
            return tokenHandler.WriteToken(token);
        }
    }
}
