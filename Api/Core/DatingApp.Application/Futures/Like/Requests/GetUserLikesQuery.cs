using DatingApp.Application.DTOs.Like;
using DatingApp.Application.Futures.Like.Responses;
using DatingApp.Application.Pagination;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Like.Requests
{
    public class GetUserLikesQuery : IRequest<RemoveLikeResponse>
    {
        public LikesParamsDto LikesParams { get; set; }
        public PaginationHeader PaginationHeader { get; set; }
    }
}
