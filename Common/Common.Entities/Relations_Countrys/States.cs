using System;
using System.Collections.Generic;

namespace Common.Entities.Relations_Countrys
{
    public class States
    {
        public States()
        {
            Cities = new HashSet<Cities>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string? FipsCode { get; set; }
        public string? Iso2 { get; set; }
        public string? Type { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public byte Flag { get; set; }
        public string? WikiDataId { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string? DeletedAt { get; set; }
        public virtual Countries Country { get; set; }
        public virtual ICollection<Cities> Cities { get; set; }
    }
}
