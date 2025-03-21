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
    public class PermissionsConfig : IEntityTypeConfiguration<Permissions>
    {
        public void Configure(EntityTypeBuilder<Permissions> builder)
        {
            builder.ToTable("Permissions");                      
            builder.HasKey("UserId", "SubModuleId");            
            builder.Property(obj => obj.UserId).IsRequired();
            builder.Property(obj => obj.Status);

            builder.Ignore(x => x.SubModule);
            builder.Ignore(x => x.User);
            builder.Ignore("ModuleId");

            builder
                .HasOne(ur => ur.SubModule)
                .WithMany(r => r.Permissions)                
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
