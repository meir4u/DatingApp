using DatingApp.Application.DTOs.Like;
using DatingApp.Application.Futures.Like.Requests;
using DatingApp.Application.Futures.Like.Responses;
using DatingApp.Application.Helpers;
using DatingApp.Application.Pagination;
using DatingApp.Application.Params;
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
    public class GetUserLikesQueryHandler : IRequestHandler<GetUserLikesQuery, GetUserLikesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserLikesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUserLikesResponse> Handle(GetUserLikesQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.LikesRepository.GetUserLikes(request.LikesParams);

            var likedUsers = users.Select(user => new LikeDto
            {
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id,

            });

            var result = await PagedList<LikeDto>.CreateAsync(likedUsers, request.LikesParams.PageNumber, request.LikesParams.PageSize);

            var response = new GetUserLikesResponse();

            response.PaginationHeader = new PaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            response.Users = result;
            return response;
        }
    }
}
