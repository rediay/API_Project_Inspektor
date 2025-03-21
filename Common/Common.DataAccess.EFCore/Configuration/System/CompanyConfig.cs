using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class CompanyConfig: BaseEntityConfig<Company>
    {
        public CompanyConfig() : base("Companies")
        {
            
        }

        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);
            
            builder.Property(obj => obj.Name);
            builder.Property(obj => obj.Identification);
            builder.Property(obj => obj.Status);
            builder.Property(obj => obj.AutoRenewal);
            builder.Property(obj => obj.Image);
            builder.Property(obj => obj.ContractDate);
        }
    }
}