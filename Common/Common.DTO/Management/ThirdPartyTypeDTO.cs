namespace Common.DTO
{
    public class ThirdPartyTypeDTO
    {
		public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }    
        public int CompanyId { get; set; }
        public int UserId{ get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
