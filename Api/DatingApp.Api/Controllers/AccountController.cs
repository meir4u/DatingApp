using AutoMapper;
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
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(
            IUserRepository userRepository, 
            ITokenService tokenService, 
            IMapper mapper

            )
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")] // Post: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await userExists(registerDto.Username))
            {
                return BadRequest("Username already taken");
            }

            var user = _mapper.Map<AppUser>(registerDto);

            using var hmac = new HMACSHA512();
            var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));

            user.UserName = registerDto.Username.ToLower();
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = hmac.Key;

            await _userRepository.AddUserAsync(user);

            return new UserDto
            {
                Username = registerDto.Username,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x=>x.IsMain)?.Url,
                KnownAs = registerDto.KnownAs,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
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
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            }; 

        }

        private async Task<bool> userExists(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return user != null;
        }
    }
}
