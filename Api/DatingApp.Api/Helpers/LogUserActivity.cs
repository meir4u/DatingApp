using DatingApp.Api.Extensions;
using DatingApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.Api.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();
            var userIdNum = userId;

            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

            var user = await repo.UserRepository.GetUseByIdAsync(userIdNum);

            user.LastActive = DateTime.UtcNow;
            await repo.Complete();
        }
    }
}
