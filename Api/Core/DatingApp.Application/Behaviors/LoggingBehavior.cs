using DatingApp.Application.Exceptions.Responses;
using DatingApp.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace DatingApp.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex) when (LogException(ex, request))
            {
                // This block will never be hit because the LogException method returns false
                throw;
            }
        }

        private bool LogException(Exception ex, TRequest request)
        {
            switch (ex)
            {
                case RepositoryException rex:
                    _logger.Error(rex, "Repository exception: {message}", rex.Message);
                    break;
                case NotFoundException nfex:
                    _logger.Warning(nfex, "Not found exception: {message}", nfex.Message);
                    break;
                case BadRequestExeption brex:
                    _logger.Warning(brex, "Bad request exception: {message}", brex.Message);
                    break;
                default:
                    _logger.Error(ex, "Unhandled exception: {message}", ex.Message);
                    break;
            }

            return false;
        }
    }
}
