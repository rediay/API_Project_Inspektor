using System;

namespace Common.DTO
{
    public class ContentPaginationFilterDTO : PaginationFilterDTO
    {
        public string ?Title { get; set; }
        public string ?Name { get; set; }
        public int? CountryId { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }
        public string ?StartDate { get; set; }
        public string ?EndDate { get; set; }
        public DateTime ?Date { get; set; }
        public string ?Document { get; set; }
    }
}