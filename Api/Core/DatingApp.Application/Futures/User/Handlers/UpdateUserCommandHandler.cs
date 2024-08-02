using AutoMapper;
using DatingApp.Application.DTOs.Member;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.User.Requests;
using DatingApp.Application.Futures.User.Responses;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.User.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateUserResponse(); 
            var username = request.Update.CurrentUsername;
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            if (user == null) throw new NotFoundException();

            _mapper.Map(request.MemberUpdate, user);

            if (await _unitOfWork.Complete()) return response;

            throw new BadRequestExeption("Failed to update user");
        }
    }
}
