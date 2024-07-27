using DatingApp.Application.Pagination;
using DatingApp.Application.Responses;
using DatingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.User.Responses
{
    public class GetUsersResponse : BaseCommandResponse
    {
        public PaginationHeader PaginationHeader { get; set; }
        public IQueryable<AppUser> Users { get; internal set; }
    }
}
