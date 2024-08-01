using DatingApp.Application.Responses;
using DatingApp.Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Application.DTOs.Like;

namespace DatingApp.Application.Futures.Like.Responses
{
    public class GetUserLikesResponse : BaseCommandResponse
    {
        public PaginationHeader PaginationHeader { get; set; }
        public PagedList<LikeDto> Users { get; internal set; }
    }
}
