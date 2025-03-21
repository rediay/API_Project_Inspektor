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
    public class AccessConfig : BaseEntityConfig<Access>
    {
        public AccessConfig() : base("Access")
        {
        }

        public override void Configure(EntityTypeBuilder<Access> builder)
        {
            base.Configure(builder);
            
            builder.Property(obj => obj.Name);            
            builder.Property(obj => obj.CompanyId).IsRequired();
            builder
                .HasMany(obj => obj.AccessSubModules)
                .WithOne()
                .HasForeignKey(obj => obj.AccessId)
                .IsRequired();      
                
                
        }
    }
}