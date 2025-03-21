/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore.Configuration;
using Common.DataAccess.EFCore.Configuration.System;
using Common.Entities;
using Common.Entities.SPsData;
using Common.Entities.BulkQuery;
using Microsoft.EntityFrameworkCore;
using Common.Entities.Relations_Countrys;

namespace Common.DataAccess.EFCore
{
    public class DataContext : DbContext
    {
        public ContextSession Session { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<ListResponse> ListResponse { get; set; }
        public DbSet<OwnListResponse> OwnListResponse { get; set; }
        public DbSet<ListGroup> ListGroups { get; set; }
        public DbSet<ListType> ListTypes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentCategory> ContentCategories { get; set; }
        public DbSet<ContentStatus> ContentStatuses { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<OwnList> OwnLists { get; set; }
        public DbSet<OwnListType> OwnListTypes { get; set; }
        public DbSet<Periodicity> Periodicities { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<QueryType> QueryType { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<QueryDetail> QueryDetails { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<CompanyTypeList> CompanyTypeList { get; set; }
        public DbSet<ThirdPartyType> ThirdPartyTypeList { get; set;}
        public DbSet<Notification> Notifications { get; set; }        
        public DbSet<EventRoi> EventRois { get; set; }
        public DbSet<EventRoiOperationStatus> EventRoiOperationStatuses { get; set; }
        public DbSet<EventRoiOperationType> EventRoiOperationTypes { get; set; }        
        public DbSet<NotificationType> NotificationTypes{ get; set; }        
        public DbSet<RiskProfileVariable> RiskProfileVariables{ get; set; }
        public DbSet<CategoryVariable> CategoryVariables{ get; set; }
        public DbSet<RiskProfile> RiskProfiles { get; set; }
        public DbSet<ThirdPartyProfiling> ThirdPartyProfiling { get; set; }
        //public DbSet<Country> Countries { get; set; }        
        public DbSet<Cities> Cities { get; set; }
        public DbSet<ThirdList> ThirdList { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<States> States { get; set; }      
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Log> Logs { get; set; }        
        public DbSet<QueryType> QueryTypes{ get; set; }
        public DbSet<AdditionalService> AdditionalServices{ get; set; }
        public DbSet<AdditionalCompanyService> AdditionalCompanyServices{ get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Modules> Modules{ get; set; }
        public DbSet<SubModules> SubModules { get; set; }
        public DbSet<Access> Access { get; set; }
        public DbSet<AccessSubModule> AccessSubModules{ get; set; }
        public DbSet<BulkQueryServicesAdditional> BulkQueryServicesAdditional { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new UserClaimConfig());
            modelBuilder.ApplyConfiguration(new UserPhotoConfig());
            modelBuilder.ApplyConfiguration(new SettingsConfig());
            
            modelBuilder.ApplyConfiguration(new CompanyConfig());
            
            modelBuilder.ApplyConfiguration(new RiskProfileVariableConfig());
            modelBuilder.ApplyConfiguration(new CategoryVariableConfig());
            
            modelBuilder.ApplyConfiguration(new ListConfig());

            modelBuilder.ApplyConfiguration(new PermissionsConfig());            
            modelBuilder.ApplyConfiguration(new ModulesConfig());
            modelBuilder.ApplyConfiguration(new AccessConfig());
            modelBuilder.ApplyConfiguration(new AccessSubModuleConfig());
            /*modelBuilder.ApplyConfiguration(new CompanyConfig());
            modelBuilder.ApplyConfiguration(new DocumentTypeConfig());
            modelBuilder.ApplyConfiguration(new ListConfig());
            modelBuilder.ApplyConfiguration(new ListGroupConfig());
            modelBuilder.ApplyConfiguration(new ListTypeConfig());
            modelBuilder.ApplyConfiguration(new MenuConfig());
            modelBuilder.ApplyConfiguration(new NewConfig());
            modelBuilder.ApplyConfiguration(new NewCategoryConfig());
            modelBuilder.ApplyConfiguration(new NewStatusConfig());
            modelBuilder.ApplyConfiguration(new NewTypeConfig());
            modelBuilder.ApplyConfiguration(new OwnListConfig());
            modelBuilder.ApplyConfiguration(new OwnListTypeConfig());
            modelBuilder.ApplyConfiguration(new PeriodicityConfig());
            modelBuilder.ApplyConfiguration(new PlanConfig());
            modelBuilder.ApplyConfiguration(new QueryConfig());
            modelBuilder.ApplyConfiguration(new QueryDetailConfig());
            modelBuilder.ApplyConfiguration(new SubMenuConfig());*/

            modelBuilder.HasDefaultSchema("starter_core");
        }
    }
}
