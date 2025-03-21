namespace Common.DTO.ThirdPartyProfiling
{
    public class RiskProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float StartValue { get; set; }
        public float EndValue { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
    }
}