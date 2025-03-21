namespace Common.Entities.SPsData
{
    public class OwnListResponse : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string TypeIdentification { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
        public string Crime { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string MoreInformation { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public string OwnListTypeName { get; set; } = string.Empty;
        //public string OwnlistTypeName { get; set; }
        public int? UserId { get; set; }
        public int CompanyId { get; set; }
        public int OwnlistTypeId { get; set; }
    }
}