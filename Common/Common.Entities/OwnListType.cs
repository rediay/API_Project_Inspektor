using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class OwnListType : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}