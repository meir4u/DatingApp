using DatingApp.Application.Futures.Like.Requests;
using DatingApp.Application.Futures.Like.Responses;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Like.Handlers
{
    public class RemoveLikeCommandHandler : IRequestHandler<RemoveLikeCommand, RemoveLikeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveLikeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RemoveLikeResponse> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
