using Common.Entities.Relations_Countrys;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class List : BaseEntity
    {
        public string Source { get; set; }
        public string Alias { get; set; }
        public string WeakAlias { get; set; }
        public string Crime { get; set; }
        public string Peps { get; set; }
        public string Zone { get; set; }
        public string Link { get; set; }
        public string MoreInformation { get; set; }
        public string Summary { get; set; }
        public string Acts { get; set; }
        public string Entity { get; set; }
        public bool Activated { get; set; }
        public bool Validated { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List TempData { get; set; }
        public int ListTypeId { get; set; }
        public int PersonTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public int UserId { get; set; }
        public int ThirdListId { get; set; }
        public int CountryId { get; set; } 
        public virtual ListType ListType { get; set; }
        public virtual PersonType PersonType { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual User User { get; set; }
        public virtual ThirdList ThirdList { get; set; }

        public List()
        {
            RelationshipNames = new[]
            {
                "ListType", "PersonType", "DocumentType", "User"
            };
        }
    }
}