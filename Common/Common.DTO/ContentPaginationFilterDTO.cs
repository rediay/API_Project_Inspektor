namespace Common.DTO
{
    public class NotificationPaginationFilterDTO : PaginationFilterDTO
    {  
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}