using DatingApp.Application.Pagination;

namespace DatingApp.Application.Params
{
    public class LikesParams : PaginationParams
    {
        public int UserId { get; set; }
        public string Predicate { get; set; }
    }
}
