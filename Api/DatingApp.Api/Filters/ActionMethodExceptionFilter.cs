using DatingApp.Application.Exceptions.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace DatingApp.Api.Filters
{
    public class ActionMethodExceptionFilter : IExceptionFilter
    {
        private readonly Serilog.ILogger _logger;

        public ActionMethodExceptionFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Handle different exception types and return appropriate responses
            context.Result = context.Exception switch
            {
                BadRequestExeption badRequestException => new BadRequestObjectResult(badRequestException.Message),
                IdentityErrorExeption identityErrorExeption => new BadRequestObjectResult(identityErrorExeption.Message),
                NotFoundException => new NotFoundResult(),
                NotAuthorizedException notAuthorizedException => new UnauthorizedObjectResult(notAuthorizedException.Message),
                _ => _handleUnexpectedException(context.Exception)
            };

            context.ExceptionHandled = true; // Mark the exception as handled
        }

        private IActionResult _handleUnexpectedException(Exception exception)
        {
            // Log the unexpected exception
            _logger.Error(exception, "An unhandled exception occurred");

            // Return a generic error response
            return new ObjectResult($"An error occurred while processing your request. Message: {exception.Message}")
            {
                StatusCode = 500
            };
        }
    }
}
