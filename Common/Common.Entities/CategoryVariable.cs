namespace Common.Entities
{
    public class CategoryVariable : BaseEntity
    {
        public string Name { get; set; }
        public float Weight { get; set; }
        public int RiskProfileVariableId { get; set; }
        public int PersonTypeId { get; set; }
        public CategoryCondition Condition { get; set; }
        
        public virtual RiskProfileVariable RiskProfileVariable { get; set; }
    }
}