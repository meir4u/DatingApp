using DatingApp.Application.DTOs.Like;
using DatingApp.Application.Futures.Like.Responses;
using DatingApp.Application.Pagination;
using DatingApp.Application.Params;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Like.Requests
{
    public class GetUserLikesQuery : IRequest<GetUserLikesResponse>
    {
        public LikesParams LikesParams { get; set; }
        public PaginationHeader PaginationHeader { get; set; }
    }
}
