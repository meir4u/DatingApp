using AutoMapper;
using DatingApp.Api.Data;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
        public async Task<ActionResult<MemberDto>> GetUserByUsername(string username)
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
    }
}
