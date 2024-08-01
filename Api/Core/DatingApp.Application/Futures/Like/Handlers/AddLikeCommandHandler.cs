using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Like.Requests;
using DatingApp.Application.Futures.Like.Responses;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Like.Handlers
{
    public class AddLikeCommandHandler : IRequestHandler<AddLikeCommand, AddLikeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddLikeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddLikeResponse> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            var response = new AddLikeResponse();

            var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(request.AddLike.Username);
            var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLikes(request.AddLike.SourceUserId);

            if (likedUser == null) throw new NotFoundException();

            if (sourceUser.UserName == request.AddLike.Username) throw new BadRequestExeption("You cannot like yourself");

            var userLike = await _unitOfWork.LikesRepository.GetUserLike(request.AddLike.SourceUserId, likedUser.Id);

            if (userLike != null) throw new BadRequestExeption("You already liked this user");

            userLike = new Domain.Entities.UserLike
            {
                SourceUserId = request.AddLike.SourceUserId,
                TargetUserId = likedUser.Id,
            };

            sourceUser.LikedUsers.Add(userLike);

            if (await _unitOfWork.Complete() == false)
            {
                throw new BadRequestExeption("Failed to like user");
            }
            return response;
        }
    }
}
