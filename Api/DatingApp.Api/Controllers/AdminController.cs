using AutoMapper;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Extensions;
using DatingApp.Api.Interfaces;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DatingApp.Api.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AdminController(
            UserManager<AppUser> userManager, 
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IMediator mediator)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            try
            {
                var result = await _mediator.Send(new GetUsersWithRolesQuery());
                return Ok(result.UserWithRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            ///////////////////////////////to remove
            //var users = await _userManager.Users
            //    .OrderBy(u => u.UserName)
            //    .Select(u => new
            //    {
            //        u.Id,
            //        Username = u.UserName,
            //        Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
            //    })
            //    .ToListAsync();
            //return Ok(users);
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery]string roles)
        {
            if (string.IsNullOrEmpty(roles)) return BadRequest("You must select at least one role");

            var selectedRoles = roles.Split(',').ToArray();

            var command = new EditRolesCommand()
            {
                EditRoles = new Application.DTOs.Account.EditRolesDto()
                {
                    Username = username,
                    Roles = selectedRoles
                }
            };

            try
            {
                var result1 = await _mediator.Send(command);
                return Ok(result1.Roles);
            }
            catch(NotFoundException ex)
            {
                return NotFound();
            }
            catch (BadRequestExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            /////////////////////to remove
            //if (string.IsNullOrEmpty(roles)) return BadRequest("You must select at least one role");

            //selectedRoles = roles.Split(',').ToArray();

            //var user = await _userManager.FindByNameAsync(username);

            //if (user == null) return NotFound();

            //var userRoles = await _userManager.GetRolesAsync(user);

            //var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            //if (!result.Succeeded) return BadRequest("Failed to add to roles");

            //result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            //if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            //return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public async Task<ActionResult<IEnumerable<PhotoForApprovalDto>>> GetPhotosForModeration()
        {
            try
            {
                var command = new GetPhotosForModerationCommand();
                var result = await _mediator.Send(command);
                return Ok(result.Photos);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //var photos = await _unitOfWork.PhotoRepository.GetUnapprovedPhotos();
            //return Ok(photos);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("approve-photo/{photoId}")]
        public async Task<ActionResult<PhotoForApprovalDto>> PhotoApproval(int photoId)
        {
            try
            {
                var command = new PhotoApprovalCommand()
                {
                    ForApproval = new Application.DTOs.Photo.PhotoForApprovalDto()
                    {
                        Id = photoId
                    }
                };
                var result = await _mediator.Send(command);
                return Ok(result.Photo);
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

            /////////////////////////
            //var photo = await _unitOfWork.PhotoRepository.GetPhotoById(photoId);

            //if(photo == null) return NotFound();

            //photo.IsApproved = true;

            //var user = await _unitOfWork.UserRepository.GetUserPhotoIdAsync(photoId);

            //if (user == null) return NotFound();

            //if (user.Photos.Any() == false) photo.IsMain = true;

            //_unitOfWork.PhotoRepository.Update(photo);

            //if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();
            //var photoDto = _mapper.Map<PhotoForApprovalDto>(photo);
            //photoDto.UserName = user.UserName;
            //return Ok(photoDto);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("reject-photo/{photoId}")]
        public async Task<ActionResult<bool>> PhotoReject(int photoId)
        {
            try
            {
                var command = new PhotoRejectCommand()
                {
                    Reject = new Application.DTOs.Photo.PhotoRejectDto()
                    {
                        PhotoId = photoId
                    }
                };
                var result = await _mediator.Send(command);
                return Ok(true);
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

            ////////////////////
            //var photo = await _unitOfWork.PhotoRepository.GetPhotoById(photoId);

            //if (photo == null) return NotFound();

            //_unitOfWork.PhotoRepository.RemovePhoto(photo);

            //if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            //return Ok(true);
        }
    }
}
