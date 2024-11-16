using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Domain.Interfaces;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Email.Base
{
    public abstract class BaseEmailHandler<TREQUEST,TRESPONSE> : IRequestHandler<TREQUEST, TRESPONSE>
        where TREQUEST : IRequest<TRESPONSE>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEnhancedEmailService _enhancedEmailService;
        protected abstract string _templateName { get; set; }
        public BaseEmailHandler(
            IUnitOfWork unitOfWork, 
            IEnhancedEmailService enhancedEmailService)
        {
            _unitOfWork = unitOfWork;
            _enhancedEmailService = enhancedEmailService;
        }

        public abstract Task<TRESPONSE> Handle(TREQUEST request, CancellationToken cancellationToken);
    }
}
