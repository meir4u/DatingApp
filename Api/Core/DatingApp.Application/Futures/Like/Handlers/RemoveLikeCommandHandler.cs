using DatingApp.Application.Exceptions.Responses;
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

        public async Task<RemoveLikeResponse> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
        {
            var response = new RemoveLikeResponse();

            var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(request.RemoveLike.Username);
            var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLikes(request.RemoveLike.SourceUserId);

            if (likedUser == null) throw new NotFoundException();

            if (sourceUser.UserName == request.RemoveLike.Username) throw new BadRequestExeption("You cannot like yourself");

            var userLike = await _unitOfWork.LikesRepository.GetUserLike(request.RemoveLike.SourceUserId, likedUser.Id);

            if (userLike == null) throw new BadRequestExeption("You did not liked this user yet.");

            userLike = new Domain.Entities.UserLike
            {
                SourceUserId = request.RemoveLike.SourceUserId,
                TargetUserId = likedUser.Id,
            };

            sourceUser.LikedUsers.Remove(userLike);

            if (await _unitOfWork.Complete() == false)
            {
                throw new BadRequestExeption("Failed to like user");
            }
            return response;
        }
    }
}
