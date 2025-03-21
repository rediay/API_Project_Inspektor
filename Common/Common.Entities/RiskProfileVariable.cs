using System.Collections.Generic;

namespace Common.Entities
{
    public class RiskProfileVariable : BaseEntity
    {
        public string Name { get; set; }
        public float Weight { get; set; }
        public int CompanyId { get; set; }
        
        public virtual Company Company { get; set; }
        public virtual ICollection<CategoryVariable> CategoryVariables { get; set; }
    }
}