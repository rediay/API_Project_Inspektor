using System;
using System.Collections.Generic;

namespace Common.Entities.Relations_Countrys
{
    public class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public decimal?Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public byte Flag { get; set; }
        public string? WikiDataId { get; set; }
        public string?CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string? DeletedAt { get; set; }
        public virtual Countries Country { get; set; }
        public virtual States State { get; set; }
    }
}
