using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class CompanyTypeList : BaseEntity
    {

        public bool Search { get; set; }
        [ForeignKey("ListType")]
        public int ListTypeId { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company {get;set; }
        public virtual ListType ListType { get; set; }
        public virtual User User { get; set; }                              

       
    }
}
