using Common.Entities.Relations_Countrys;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
	public class ListType : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
        [ForeignKey("ListGroup")]
        public int ListGroupId { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [ForeignKey("Periodicity")]
        public int PeriodicityId { get; set; }

		public virtual Periodicity? Periodicity { get; set; }
		public virtual ListGroup? ListGroup { get; set; }
		public virtual Countries? Country { get; set; }
	}
}

