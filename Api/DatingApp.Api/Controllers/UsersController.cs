using AutoMapper;
using DatingApp.Api.Data;
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

namespace DatingApp.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;

        public UsersController(
            IPhotoService photoService,
            IMediator mediator)
        {
            _photoService = photoService;
            _mediator = mediator;
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
            ////////////////////////////////////////////////////////////
            //try
            //{
            //    var gender = await _unitOfWork.UserRepository.GetUserGender(User.GetUsername());
            //    userParams.CurrentUsername = User.GetUsername();

            //    if(string.IsNullOrEmpty(userParams.Gender))
            //    {
            //        userParams.Gender = gender == "male" ? "female" : "male";
            //    }
            //    var users = await _unitOfWork.UserRepository.GetMembersAsync(userParams);

            //    Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            //    return Ok(users);

            //}catch(Exception ex)
            //{
            //    #if DEBUG

            //    return BadRequest(ex.Message);
                
            //    #else
                
            //    // In release mode, return a generic BadRequest response
            //    return BadRequest("An error occurred while processing your request.");
                
            //    #endif
            //}
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


//            try
//            {
//                var user = await _unitOfWork.UserRepository.GetMemberAsync(username, User.GetUsername());
//                return Ok(user);

//            }
//            catch (Exception ex)
//            {
//#if DEBUG

//                return BadRequest(ex.Message);

//#else
                
//                // In release mode, return a generic BadRequest response
//                return BadRequest("An error occurred while processing your request.");
                
//#endif
//            }
        }

        [HttpPut]
        public async Task<ActionResult<MemberDto>> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            try
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

            //////////////////////////////////////////////////////
//            try
//            {
//                var username = User.GetUsername();
//                var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
//                if(user == null) return NotFound();

//                _mapper.Map(memberUpdateDto, user);

//                if (await _unitOfWork.Complete()) return NoContent();

//                return BadRequest("Failed to update user");

//            }
//            catch (Exception ex)
//            {
//#if DEBUG

//                return BadRequest(ex.Message);

//#else
                
//                // In release mode, return a generic BadRequest response
//                return BadRequest("An error occurred while processing your request.");
                
//#endif
//            }
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
            try
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

            ////////////////////////////////////
            //var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            //if (user == null) return NotFound();

            //var photo = user.Photos.FirstOrDefault(x=>x.Id == photoId);

            //if (photo == null) return NotFound();

            //if (photo.IsMain) return BadRequest("this is already your main photo");

            //var currentMain = user.Photos.FirstOrDefault(x=>x.IsMain);

            //if(currentMain != null) currentMain.IsMain = false;

            //photo.IsMain = true;

            //if(await _unitOfWork.Complete()) return NoContent();

            //return BadRequest("Problem Setting main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            try
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

            ////////////////////////////
            //var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            ////var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            //var photo = await _unitOfWork.PhotoRepository.GetPhotoById(photoId);

            //if(photo == null) return NotFound();

            //if (photo.IsMain) return BadRequest("You cannot delete your main photo");

            //if(photo.PublicId != null)
            //{
            //    var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            //    if (result.Error != null) return BadRequest(result.Error.Message);
            //}

            //user.Photos.Remove(photo);
            
            //if(await _unitOfWork.Complete()) return Ok();

            //return BadRequest("Problem deleting photo");
        }
    }
}
