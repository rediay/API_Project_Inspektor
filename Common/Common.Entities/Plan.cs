
using Common.Entities.Relations_Countrys;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
	public class Plan : BaseEntity
	{
		public string Name { get; set; }
		public bool Type { get; set; }
		public int QtyQueries { get; set; }
		public float Price { get; set; }
		public bool Status { get; set; }
		
		[ForeignKey("Country")]
		public int CountryId { get; set; }
		public virtual Countries Country {get;set;}
		[ForeignKey("User")]		
		public int UserId { get; set; }
		public virtual User User { get; set; }
		
		public Plan()
		{
			RelationshipNames = new[]
			{
				"Country", "User"
			};
		}
	}
}

