using Common.DTO.Relations_Countrys;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.DTO
{
    public class ListTypeDTO 
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
        public int ListGroupId { get; set; }
        public int CountryId { get; set; }
        public int PeriodicityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ListGroupDTO? ListGroup { get; set; }
		public virtual CountryDTO? Country { get; set; }
        public virtual PeriodicityDTO? Periodicity { get; set; }

        [NotMapped()][JsonIgnore] public string[] RelationshipNames { get; set; } = { };
    }
}

