using DatingApp.Api.Data;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")] // Post: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();

            try
            {
                if(await userExists(registerDto.Username))
                {
                    return BadRequest("Username already taken");
                }

                var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));

                var user = new AppUser
                {
                    UserName = registerDto.Username.ToLower(),
                    PasswordHash = hashedPassword,
                    PasswordSalt = hmac.Key,
                };
                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return new UserDto
                {
                    Username = registerDto.Username,
                    Token = _tokenService.CreateToken(user),
                };

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == loginDto.Username.ToLower());
            if(user == null)
            {
                return Unauthorized("Invalid username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(var i = 0; i < computedhash.Length; i++)
            {
                if (computedhash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
            }; ;

        }

        private async Task<bool> userExists(string username)
        {
            return await _context.Users.AnyAsync(x=>x.UserName.ToLower() == username.ToLower());
        }
    }
}
