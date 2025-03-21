namespace Common.Entities
{
    public class ThirdPartyProfiling : BaseEntity
    {
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string Document { get; set; }
        public string PersonType { get; set; }
        public float Score { get; set; }
        public string Type { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}