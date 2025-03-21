namespace Common.DTO.RestrictiveLists
{
    public class ListPaginationFilterDTO : PaginationFilterDTO
    {
        public string thirdPartyId {  get; set; }
        public string Alias { get; set; }
        public string Document { get; set; }
        public string Entity { get; set; }
        public string Source { get; set; }
        public string Zone { get; set; }
        public bool? Activated { get; set; }
        public bool? Validated { get; set; }
        public int? ListTypeId { get; set; }
        public int? UserId { get; set; }
        public int? CountryId { get; set; }
    }
}