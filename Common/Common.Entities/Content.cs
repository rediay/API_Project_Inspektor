using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Content : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public int CountryId { get; set; }
        public int ContentCategoryId { get; set; }
        public int ContentStatusId { get; set; }
        public int ContentTypeId { get; set; }
        public virtual User User { get; set; }
        public virtual ContentCategory ContentCategory { get; set; }
        public virtual ContentType ContentType { get; set; }
        public virtual ContentStatus ContentStatus { get; set; }
    }
}