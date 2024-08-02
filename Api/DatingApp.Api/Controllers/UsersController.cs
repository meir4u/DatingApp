using AutoMapper;
using DatingApp.Api.Data;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Extensions;
using DatingApp.Api.Helpers;
using DatingApp.Api.Interfaces;
using DatingApp.Application.DTOs.Register;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DatingApp.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;

        public UsersController(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IPhotoService photoService,
            IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
            _mediator = mediator;
        }


        [HttpGet(Name = "Users")]
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            try
            {
                var gender = await _unitOfWork.UserRepository.GetUserGender(User.GetUsername());
                userParams.CurrentUsername = User.GetUsername();

                if(string.IsNullOrEmpty(userParams.Gender))
                {
                    userParams.Gender = gender == "male" ? "female" : "male";
                }
                var users = await _unitOfWork.UserRepository.GetMembersAsync(userParams);

                Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

                return Ok(users);

            }catch(Exception ex)
            {
                #if DEBUG

                return BadRequest(ex.Message);
                
                #else
                
                // In release mode, return a generic BadRequest response
                return BadRequest("An error occurred while processing your request.");
                
                #endif
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
            try
            {
                var user = await _unitOfWork.UserRepository.GetMemberAsync(username, User.GetUsername());
                return Ok(user);

            }
            catch (Exception ex)
            {
#if DEBUG

                return BadRequest(ex.Message);

#else
                
                // In release mode, return a generic BadRequest response
                return BadRequest("An error occurred while processing your request.");
                
#endif
            }
        }

        [HttpPut]
        public async Task<ActionResult<MemberDto>> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            try
            {
                var username = User.GetUsername();
                var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
                if(user == null) return NotFound();

                _mapper.Map(memberUpdateDto, user);

                if (await _unitOfWork.Complete()) return NoContent();

                return BadRequest("Failed to update user");

            }
            catch (Exception ex)
            {
#if DEBUG

                return BadRequest(ex.Message);

#else
                
                // In release mode, return a generic BadRequest response
                return BadRequest("An error occurred while processing your request.");
                
#endif
            }
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            try
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
            catch (BadRequestExeption ex)
            {
                throw new BadRequestExeption("Username already taken");
            }
            catch (IdentityErrorExeption ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            /////////////////
            //var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            //if(user == null) return NotFound();

            //var result = await _photoService.AddPhotoAsync(file);

            //if(result.Error != null) return BadRequest(result.Error);


            //var photo = new Photo
            //{
            //    Url = result.SecureUrl.AbsoluteUri,
            //    PublicId = result.PublicId,
            //};

            //user.Photos.Add(photo);

            //if(await _unitOfWork.Complete() )
            //{
            //    return CreatedAtAction(nameof(GetUser), new {username = user.UserName}, _mapper.Map<PhotoDto>(photo));
            //}

            //return BadRequest("Problem adding photo");

        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var photo = user.Photos.FirstOrDefault(x=>x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("this is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x=>x.IsMain);

            if(currentMain != null) currentMain.IsMain = false;

            photo.IsMain = true;

            if(await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Problem Setting main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            //var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            var photo = await _unitOfWork.PhotoRepository.GetPhotoById(photoId);

            if(photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete your main photo");

            if(photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);
            
            if(await _unitOfWork.Complete()) return Ok();

            return BadRequest("Problem deleting photo");
        }
    }
}
