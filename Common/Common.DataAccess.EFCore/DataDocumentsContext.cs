/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore.Configuration;
using Common.DataAccess.EFCore.Configuration.System;
using Common.Entities;
using Common.Entities.SPsData;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore
{
    public class DataDocumentsContext : DbContext
    {

        public ContextSession Session { get; set; }
        public DbSet<NombreCedula> NombreCedula { get; set; }

        public DataDocumentsContext(DbContextOptions<DataDocumentsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // base.OnModelCreating(modelBuilder);
        }
    }
}
