namespace Common.Entities
{
    public class RiskProfile : BaseEntity
    {
        public string Name { get; set; }
        public float StartValue { get; set; }
        public float EndValue { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}