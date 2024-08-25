using DatingApp.Application.Enums;
using DatingApp.Application.Pagination;

namespace DatingApp.Application.Params
{
    public class LikesParams : PaginationParams
    {
        public int UserId { get; set; }
        public EPredicate.Like Predicate { get; set; }
    }
}
