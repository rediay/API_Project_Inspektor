using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Common.Entities
{
    public class Company : BaseEntity
	{
		public string Name { get; set; }
		public string Identification { get; set; }
		public bool Status { get; set; }
		public bool AutoRenewal { get; set; }
		public string Image { get; set; }
		public string ContractDate { get; set; }
		[ForeignKey("Plan")]
		public int? PlanId { get; set; }
		public virtual Plan Plan { get; set; }
		public virtual ICollection<User> users { get; set; }

		
	
	}
}

