using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class OwnList : BaseEntity
    {
        public string Name { get; set; }
        public string Identification { get; set; }
        public string TypeIdentification { get; set; }
        public string DocumentType { get; set; }
        public string Source { get; set; }
        public string Alias { get; set; }
        public string Crime { get; set; }
        public string Link { get; set; }
        public string MoreInformation { get; set; }
        public string Zone { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [ForeignKey("OwnListType")]
        public int? OwnListTypeId { get; set; }
        public virtual OwnListType OwnListType { get; set; }
    }
}