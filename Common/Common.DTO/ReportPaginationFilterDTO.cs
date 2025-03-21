using System;

namespace Common.DTO
{
    public class ReportPaginationFilterDTO : PaginationFilterDTO
    {
        public string Name { get; set; }
        public string User { get; set; }
        public string Identification { get; set; }
        public int? IdQueryCompany { get; set; }
        public int? QueryTypeId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}