using DatingApp.Api.Data;
using DatingApp.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet(Name = "Users")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
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

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                return Ok(user);

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
    }
}
