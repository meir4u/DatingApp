using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace DatingApp.Api.Filters
{
    public class ActionMethodLoggingFilter : IAsyncActionFilter
    {
        private readonly Serilog.ILogger _logger;

        public ActionMethodLoggingFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Log request details
            var request = context.HttpContext.Request;
            request.EnableBuffering();

            var requestBody = await ReadRequestBodyAsync(request);
            _logger.Information("Incoming Request: {Method} {Path} {Body}", request.Method, request.Path, requestBody);

            // Proceed to the next action
            var executedContext = await next();

            try
            {
                // Log response details
                if (executedContext.Result is ObjectResult objectResult)
                {
                    // Ensure the data is fully fetched and materialized
                    if (objectResult.Value is IQueryable queryable)
                    {
                        objectResult.Value = queryable.Cast<object>().ToList();
                    }

                    var jsonSettings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };

                    var responseBody = objectResult.Value != null ? Newtonsoft.Json.JsonConvert.SerializeObject(objectResult.Value, jsonSettings) : "No Content";
                    _logger.Information("Outgoing Response: {StatusCode} {Body}", objectResult.StatusCode, responseBody);
                }
                else
                {
                    _logger.Information("Outgoing Response: {StatusCode}", executedContext.HttpContext.Response.StatusCode);
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
            
        }

        private async Task<string> ReadRequestBodyAsync(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            request.Body.Position = 0;

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return body;
            }
        }
    }
}
