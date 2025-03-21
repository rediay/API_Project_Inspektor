using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class ContentCategory : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("ContentTypesId")]
        public virtual int ContentTypesId { get; set; }
        public virtual User User { get; set; }
    }
}