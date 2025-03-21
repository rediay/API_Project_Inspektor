using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class CategoryVariableConfig : BaseEntityConfig<CategoryVariable>
    {
        public CategoryVariableConfig() : base("CategoryVariables")
        {
        }

        public override void Configure(EntityTypeBuilder<CategoryVariable> builder)
        {
            base.Configure(builder);
            
            builder.Property(obj => obj.Name).IsRequired();
            builder.Property(obj => obj.Weight).IsRequired();
            builder.Property(application => application.Condition)
                .HasColumnName("Condition")
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<CategoryCondition>(v));
        }
    }
}