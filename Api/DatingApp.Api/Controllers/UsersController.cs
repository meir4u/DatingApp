using AutoMapper;
using DatingApp.Api.Data;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Extensions;
using DatingApp.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DatingApp.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet(Name = "Users")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetMembersAsync();
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
                var user = await _userRepository.GetMemberAsync(username);
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
                var user = await _userRepository.GetUserByUsernameAsync(username);
                if(user == null) return NotFound();

                _mapper.Map(memberUpdateDto, user);

                if (await _userRepository.SaveAllAsync()) return NoContent();

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
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            if(user == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            if(user.Photos.Any() == false) photo.IsMain = true;

            user.Photos.Add(photo);

            if(await _userRepository.SaveAllAsync() )
            {
                return CreatedAtAction(nameof(GetUser), new {username = user.UserName}, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem adding photo");

        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var photo = user.Photos.FirstOrDefault(x=>x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("this is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x=>x.IsMain);

            if(currentMain != null) currentMain.IsMain = false;

            photo.IsMain = true;

            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Problem Setting main photo");
        }
    }
}
