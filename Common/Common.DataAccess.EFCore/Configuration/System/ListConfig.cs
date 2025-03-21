using System.Collections.Generic;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class ListConfig: BaseEntityConfig<List>
    {
        public ListConfig() : base("Lists")
        {
            
        }
        
        public override void Configure(EntityTypeBuilder<List> builder)
        {
            base.Configure(builder);
            
            builder.Property(application => application.TempData)
                .HasColumnName("TempData")
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List>(v));
        }
    }
}