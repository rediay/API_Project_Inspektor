using System;

namespace Common.Entities
{
    public class AdditionalCompanyService : BaseEntity
    {
        public int CompanyId { get; set; }
        public int AdditionalServiceId { get; set; }
        public bool Active { get; set; }
        public virtual Company Company { get; set; }
        public virtual AdditionalService AdditionalService { get; set; }
        
        public AdditionalCompanyService()
        {
            RelationshipNames = new[]
            {
                "Company", "AdditionalService"
            };
        }
    }
}