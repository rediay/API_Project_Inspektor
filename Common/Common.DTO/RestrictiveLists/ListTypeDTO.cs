using System.Collections.Generic;
using Common.DTO.Relations_Countrys;
using Common.Entities;

namespace Common.DTO.RestrictiveLists
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
        public string Color { get; set; }
        
        public ListGroupDTO ListGroup { get; set; }
        public CountryDTO Country { get; set; }
        public PeriodicityDTO Periodicity { get; set; }
        public List<CountryDTO> Countries { get; set; }
        public List<ListGroupDTO> ListGroups { get; set; }
        public List<PeriodicityDTO> Periodicities { get; set; }
    }
}