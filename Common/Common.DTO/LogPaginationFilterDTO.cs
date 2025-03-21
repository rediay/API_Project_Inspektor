using System;

namespace Common.DTO.Log
{
    public class LogPaginationFilterDTO : PaginationFilterDTO
    {
        public string User { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}