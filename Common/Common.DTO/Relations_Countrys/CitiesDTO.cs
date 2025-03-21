using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Relations_Countrys
{
    public class CitiesDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name es required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "StateId es required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "StateCode es required")]
        public string StateCode { get; set; }
        [Required(ErrorMessage = "CountryId es required")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "CountryCode es required")]
        public string CountryCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal Longitude { get; set; }
        [Required(ErrorMessage = "Flag es required")]
        public byte Flag { get; set; }
        public string? WikiDataId { get; set; }
        public string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string? DeletedAt { get; set; }
    }
}
