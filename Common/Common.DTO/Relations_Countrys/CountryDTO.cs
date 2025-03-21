
using System;

namespace Common.DTO
{
    public class CountryDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string NumericCode { get; set; }
        public string Iso2 { get; set; }
        public string PhoneCode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string Tld { get; set; }
        public string Native { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public string Timezones { get; set; }
        public string Translations { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
        public byte Flag { get; set; }
        public string WikiDataId { get; set; }
    }
}
