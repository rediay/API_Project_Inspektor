namespace Common.DTO
{
    public class PaginationFilterDTO
    {
        public string query { get; set; }
        public int PerPage { get; set; }
        public int PageNumber { get; set; }
        public bool ShowAll { get; set; }

        public PaginationFilterDTO()
        {
            PageNumber = 1;
            PerPage = 10;
        }

        public PaginationFilterDTO(int pageNumber, int perPage)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PerPage = perPage > 10 ? 10 : perPage;
        }

        public PaginationFilterDTO(string query, int perPage, int pageNumber)
        {
            this.query = query;
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PerPage = perPage > 10 ? 10 : perPage;
        }
    }
}