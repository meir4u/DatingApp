using AutoMapper;
using DatingApp.Api.Extensions;
using DatingApp.Application.DTOs.Member;
using DatingApp.Application.DTOs.Photo;
using DatingApp.Application.DTOs.Register;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Application.Futures.User.Requests;
using DatingApp.Application.Pagination;
using DatingApp.Application.Params;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using ILogger = Serilog.ILogger;

namespace DatingApp.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public UsersController(
            IPhotoService photoService,
            IMediator mediator,
            ILogger logger)
        {
            _photoService = photoService;
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet(Name = "Users")]
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            try
            {
                var command = new GetUsersQuery()
                {
                    Params = userParams
                };
                command.Params.CurrentUsername = User.GetUsername();
                var result = await _mediator.Send(command);
                Response.AddPaginationHeader(result.PaginationHeader);
                return Ok(result.Users);
            }
            catch (BadRequestExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //        [HttpGet("{id}")]
        //        public async Task<ActionResult<MemberDto>> GetUser(int id)
        //        {
        //            try
        //            {
        //                var user = await _userRepository.GetUseByIdAsync(id);
        //                var userToReturn = _mapper.Map<MemberDto>(user);
        //                return Ok(userToReturn);

        //            }catch(Exception ex)
        //            {
        //#if DEBUG

        //                return BadRequest(ex.Message);

        //#else

        //                // In release mode, return a generic BadRequest response
        //                return BadRequest("An error occurred while processing your request.");

        //#endif
        //            }
        //        }


        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var command = new GetUserQuery()
            {
                GetUser = new Application.DTOs.User.GetUserDto()
                {
                    Username = username,
                    CurrentUser = User.GetUsername()
                }
            };
            var result = await _mediator.Send(command);
            return Ok(result.User);
        }

        [HttpPut]
        public async Task<ActionResult<MemberDto>> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var command = new UpdateUserCommand()
            {
                MemberUpdate = memberUpdateDto,
                Update = new Application.DTOs.User.UpdateUserDto()
                {
                    CurrentUsername = User.GetUsername()
                }
            };
            var result = await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var command = new AddPhotoCommand()
            {
                Add = new Application.DTOs.Photo.AddPhotoDto()
                {
                    File = file,
                    Username = User.GetUsername()
                },
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { username = result.Username }, result.Photo);

        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var command = new SetMainPhotoCommand()
            {
                SetMainPhoto = new Application.DTOs.Photo.SetMainPhotoDto()
                {
                    PhotoId = photoId,
                    Username = User.GetUsername()
                }
            };
            var result = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var command = new DeletePhotoCommand()
            {
                Delete = new Application.DTOs.Photo.DeletePhotoDto()
                {
                    Username = User.GetUsername(),
                    PhotoId = photoId
                }
            };
            var result = await _mediator.Send(command);
            return Ok();
        }
    }
}
