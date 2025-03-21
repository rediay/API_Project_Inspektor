using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Queries
{
    public class BulkQueryListExcel
    {
        public string NameQuery { get; set; }
        public string IdentificationQuery { get; set; }

        public string IdQueryCompany { get; set; }

        public string Name { get; set; }
        public string Identification { get; set; }
        public string Document { get; set; }
        public string Source { get; set; }
        public string KindPerson { get; set; }
        public string Alias { get; set; }
        public string WeakAlias { get; set; }
        public string Crime { get; set; }
        public string Peps { get; set; }
        public string Zone { get; set; }
        public string Link { get; set; }
        public string MoreInformation { get; set; }
        public bool Status { get; set; }
        public string Summary { get; set; }
        public string Acts { get; set; }
        public string Entity { get; set; }
        public bool Activated { get; set; }
        public bool Validated { get; set; }

        public int ListGroupId { get; set; }
        public string NameTypeDocument { get; set; }
        public string NameListType { get; set; }
        public string NameListGroup { get; set; }
        public int PriorityResult { get; set; }
        public int Order { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ListTypeId { get; set; }
        public int PersonTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
