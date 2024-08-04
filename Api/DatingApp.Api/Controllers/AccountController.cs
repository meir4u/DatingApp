using AutoMapper;
using DatingApp.Application.DTOs.Account;
using DatingApp.Application.DTOs.Register;
using DatingApp.Application.DTOs.User;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AccountController(
            IMediator mediator

            )
        {
            _mediator = mediator;
        }

        [HttpPost("register")] // Post: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            try
            {
                var command = new RegisterCommand()
                {
                    Register = registerDto,
                };
                var result = await _mediator.Send(command);
                return Ok(result.User);
            }
            catch(BadRequestExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IdentityErrorExeption ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            ////////////////to remove
            ///
            //if (await userExists(registerDto.Username))
            //{
            //    return BadRequest("Username already taken");
            //}
            //var user = _mapper.Map<AppUser>(registerDto);

            //user.UserName = registerDto.Username.ToLower();

            //var result = await _userManager.CreateAsync(user, registerDto.Password);

            //if (!result.Succeeded) return BadRequest(result.Errors);

            //var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            //if(!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            //return new UserDto
            //{
            //    Username = registerDto.Username,
            //    Token = await _tokenService.CreateToken(user),
            //    PhotoUrl = user.Photos.FirstOrDefault(x=>x.IsMain)?.Url,
            //    KnownAs = registerDto.KnownAs,
            //    Gender = user.Gender,
            //};
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                var command = new LoginCommand()
                {
                    Login = loginDto
                };
                var result = await _mediator.Send(command);
                return Ok(result.User);
            }
            catch(NotAuthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //////////////////////////////////////to remove
            //var user = await _userManager.Users
            //                    .Include(p => p.Photos)
            //                    .SingleOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());
            //if(user == null)
            //{
            //    return Unauthorized("Invalid username");
            //}

            //var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            //if (!result) return Unauthorized("Invalid password");

            //return new UserDto
            //{
            //    Username = user.UserName,
            //    Token = await _tokenService.CreateToken(user),
            //    PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            //    KnownAs = user.KnownAs,
            //    Gender = user.Gender,
            //}; 

        }

        //private async Task<bool> userExists(string username)
        //{
        //    var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
        //    return user != null;
        //}
    }
}
