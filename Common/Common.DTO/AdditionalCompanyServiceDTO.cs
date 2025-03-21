using Common.Entities;

namespace Common.DTO
{
    public class AdditionalCompanyServiceDTO : BaseDTO
    {
        public int CompanyId { get; set; }
        public int AdditionalServiceId { get; set; }
        public bool Active { get; set; }
        public virtual Company Company { get; set; }
        public virtual AdditionalService AdditionalService { get; set; }
    }
}