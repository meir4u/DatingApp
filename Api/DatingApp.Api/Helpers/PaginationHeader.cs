namespace DatingApp.Api.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPages, int itemsPerPage, int totalItems, int totalPages)
        {
            CurrentPages = currentPages;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int CurrentPages { get; set; }   
        public int ItemsPerPage { get; set; }
        public int TotalItems{ get; set; }
        public int TotalPages { get; set; }
    }
}
