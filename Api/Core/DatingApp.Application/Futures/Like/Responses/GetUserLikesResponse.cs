using DatingApp.Application.Responses;
using DatingApp.Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Like.Responses
{
    public class RemoveLikeResponse : BaseCommandResponse
    {
        public PaginationHeader PaginationHeader { get; set; }
    }
}
