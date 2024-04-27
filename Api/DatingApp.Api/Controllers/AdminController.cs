using AutoMapper;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Extensions;
using DatingApp.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();
            return Ok(users);
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery]string roles)
        {
            if (string.IsNullOrEmpty(roles)) return BadRequest("You must select at least one role");

            var selectedRoles = roles.Split(',').ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public async Task<ActionResult<IEnumerable<PhotoForApprovalDto>>> GetPhotosForModeration()
        {
            var photos = await _unitOfWork.PhotoRepository.GetUnapprovedPhotos();
            return Ok(photos);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("approve-photo/{photoId}")]
        public async Task<ActionResult<PhotoForApprovalDto>> PhotoApproval(int photoId)
        {
            var photo = await _unitOfWork.PhotoRepository.GetPhotoById(photoId);

            if(photo == null) return NotFound();

            photo.IsApproved = true;

            var user = await _unitOfWork.UserRepository.GetUserPhotoIdAsync(photoId);

            if (user == null) return NotFound();

            if (user.Photos.Any() == false) photo.IsMain = true;

            _unitOfWork.PhotoRepository.Update(photo);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();
            var photoDto = _mapper.Map<PhotoForApprovalDto>(photo);
            photoDto.UserName = user.UserName;
            return Ok(photoDto);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("reject-photo/{photoId}")]
        public async Task<ActionResult<bool>> PhotoReject(int photoId)
        {
            var photo = await _unitOfWork.PhotoRepository.GetPhotoById(photoId);

            if (photo == null) return NotFound();

            _unitOfWork.PhotoRepository.RemovePhoto(photo);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            return Ok(true);
        }
    }
}
