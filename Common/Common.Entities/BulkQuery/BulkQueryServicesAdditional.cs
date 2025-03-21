using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.BulkQuery
{
    public class BulkQueryServicesAdditional
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
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
    }
}
