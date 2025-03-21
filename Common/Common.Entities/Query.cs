using System.Collections.Generic;

namespace Common.Entities
{
    public class Query : BaseEntity
    {
        public int IdQueryCompany { get; set; }
        public int CompanyId { get; set; }
        public string ThirdTypeName { get; set; }
        public int QueryTypeId { get; set; }
        public virtual QueryType QueryType { get; set; }
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<QueryDetail> QueryDetails { get; set;}
    }
}
