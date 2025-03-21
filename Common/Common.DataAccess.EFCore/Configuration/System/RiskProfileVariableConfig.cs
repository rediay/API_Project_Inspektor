using Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class RiskProfileVariableConfig : BaseEntityConfig<RiskProfileVariable>
    {
        public RiskProfileVariableConfig() : base("RiskProfileVariables")
        {
        }

        public override void Configure(EntityTypeBuilder<RiskProfileVariable> builder)
        {
            base.Configure(builder);
            builder.Property(obj => obj.Name).IsRequired();
            builder.Property(obj => obj.Weight).IsRequired();
        }
    }
}