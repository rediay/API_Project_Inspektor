namespace Common.DTO.ThirdPartyProfiling
{
    public class CategoryVariableDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public int RiskProfileVariableId { get; set; }
        public int PersonTypeId { get; set; }
        public int CompanyId { get; set; }

        public CategoryConditionDTO Condition { get; set; }
        public RiskProfileVariableDTO RiskProfileVariable { get; set; }
    }
}