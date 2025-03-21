/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class ModulesConfig : BaseEntityConfig<Modules>
    {
        public ModulesConfig() : base("Modules") { }

        public override void Configure(EntityTypeBuilder<Modules> builder)
        {
            base.Configure(builder);
            builder.Property(obj => obj.Title);
            builder.Property(obj => obj.icon);
            
            

  


        }
    }
}
