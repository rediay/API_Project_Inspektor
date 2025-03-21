using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.DTO
{
	public class CompanyTypeListDTO
	{
		public int Id { get; set; }
		public bool Search { get; set; }
        [NotMapped] public string? Name { get; set; } 
		public int ListTypeId { get; set; }
		public int UserId { get; set; }
		public int CompanyId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }

		public virtual ListTypeDTO ListType { get; set; }
		
	}
}
