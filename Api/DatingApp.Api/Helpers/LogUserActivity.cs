using DatingApp.Api.Extensions;
using DatingApp.Application.DTOs.Account;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.Api.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        private readonly IMediator _mediator;

        public LogUserActivity(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var command = new UserLastActiveUpdateCommand()
            {
                UserLastActiveUpdate = new UserLastActiveUpdateDto()
                {
                    UserId = resultContext.HttpContext.User.GetUserId(),
                    IsAuthenticated = resultContext.HttpContext.User.Identity.IsAuthenticated,
                }
            };

            await _mediator.Send(command);
        }
    }
}
