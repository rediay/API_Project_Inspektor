/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class AccessSubModuleConfig : IEntityTypeConfiguration<AccessSubModule>
    {
        public void Configure(EntityTypeBuilder<AccessSubModule> builder)
        {
            builder.ToTable("AccessSubModules");
            builder.HasKey("AccessId", "SubModuleId");
            builder.Property(obj => obj.AccessId).IsRequired();
            builder.Property(obj => obj.SubModuleId).IsRequired();

            builder.Ignore(x => x.SubModule);
            builder.Ignore(x => x.Access);

            builder
                .HasOne(ur => ur.SubModule)
                .WithMany(r => r.AccessSubModules)
                .HasForeignKey(r => r.SubModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
