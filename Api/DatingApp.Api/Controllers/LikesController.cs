using DatingApp.Api.Extensions;
using DatingApp.Application.DTOs.Like;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Like.Requests;
using DatingApp.Application.Pagination;
using DatingApp.Application.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ILogger = Serilog.ILogger;

namespace DatingApp.Api.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public LikesController(
            IMediator mediator,
            ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var command = new AddLikeCommand()
            {
                AddLike = new AddLikeDto()
                {
                    SourceUserId = User.GetUserId(),
                    Username = username,
                }
            };

            try
            {
                var result = await _mediator.Send(command);
                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound();
            }catch(BadRequestExeption ex)
            {
                return BadRequest(ex.Message);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
        {

            likesParams.UserId = User.GetUserId();

            var query = new GetUserLikesQuery()
            {
                LikesParams = likesParams
            };
            var result = await _mediator.Send(query);
            Response.AddPaginationHeader(new PaginationHeader(result.Users.CurrentPage, result.Users.PageSize, result.Users.TotalCount, result.Users.TotalPages));
            return Ok(result.Users);
        }

    }
}
