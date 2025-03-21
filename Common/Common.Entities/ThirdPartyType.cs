using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class ThirdPartyType : BaseEntity
    {
                
        public string Name { get; set; }
        public bool Status { get; set; }       
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }

    }
}
