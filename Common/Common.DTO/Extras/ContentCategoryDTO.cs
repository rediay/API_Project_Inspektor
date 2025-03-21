using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.DTO
{
    public class ContentCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContentTypesId { get; set; }
        public User User { get; set; }
        public string DeletedAt { get; set; }
        public string CreatedAt { get; set; }
        [NotMapped][JsonIgnore] public string[] RelationshipNames { get; set; } = { };
        public string UpdatedAt { get; set; }
    }
}