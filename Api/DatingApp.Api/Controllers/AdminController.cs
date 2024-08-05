using AutoMapper;
using DatingApp.Api.Extensions;
using DatingApp.Application.DTOs.Photo;
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
using ILogger = Serilog.ILogger;

namespace DatingApp.Api.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public AdminController(
            UserManager<AppUser> userManager,
            IMediator mediator,
            ILogger logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _logger = logger;
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
        }
    }
}
