using System;

namespace Common.DTO.Queries
{
    public class BulkQueryServicesAdditionalDTO
    {
        public int Id { get; set; }
        public bool attorneyService { get; set; }
        public bool judicialBranchService { get; set; }
        public bool jempsJudicialBranchService { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ConsultingStatus { get; set; }
        public int CurrentConsulting { get; set; }
        public int TotalConsulting { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public UserDTO User { get; set; }
        public CompanyDTO Company { get; set; }
        public string Process { get { return $"{CurrentConsulting} / {TotalConsulting}"; } }
    }
}
