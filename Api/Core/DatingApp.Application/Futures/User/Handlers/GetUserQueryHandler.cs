using DatingApp.Application.Futures.User.Requests;
using DatingApp.Application.Futures.User.Responses;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.User.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var response= new GetUserResponse();

            var user = await _unitOfWork.UserRepository.GetMemberAsync(request.GetUser.Username, request.GetUser.CurrentUser);
            response.User = user;

            return response;
        }
    }
}
