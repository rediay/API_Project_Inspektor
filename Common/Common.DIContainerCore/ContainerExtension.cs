/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore;
using Common.DataAccess.EFCore.Repositories;
using Common.DataAccess.EFCore.Repositories.Document;
using Common.DataAccess.EFCore.Repositories.Extras;
using Common.DataAccess.EFCore.Repositories.Management;
using Common.DataAccess.EFCore.Repositories.Notifications;
using Common.DataAccess.EFCore.Repositories.OwnLists;
using Common.DataAccess.EFCore.Repositories.RestrictiveLists;
using Common.DataAccess.EFCore.Repositories.Queries;
using Common.DataAccess.EFCore.Repositories.ThirdPartyProfiling;
using Common.DataAccess.EFCore.Repositories.Users;
using Common.Entities;
using Common.Services;
using Common.Services.Document;
using Common.Services.Extras;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Document;
using Common.Services.Infrastructure.Mail;
using Common.Services.Infrastructure.Management;
using Common.Services.Infrastructure.OwnLists;
using Common.Services.Infrastructure.Queries;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Repositories.Extras;
using Common.Services.Infrastructure.Repositories.Management;
using Common.Services.Infrastructure.Repositories.Notifications;
using Common.Services.Infrastructure.Repositories.OwnLists;
using Common.Services.Infrastructure.Repositories.RestrictiveLists;
using Common.Services.Infrastructure.Repositories.ThirdPartyProfiling;
using Common.Services.Infrastructure.Repositories.Users;
using Common.Services.Infrastructure.Services;
using Common.Services.Infrastructure.Services.Extras;
using Common.Services.Infrastructure.Services.Notifications;
using Common.Services.Infrastructure.Services.RestrictiveLists;
using Common.Services.Infrastructure.Services.ThirdPartyProfiling;
using Common.Services.Infrastructure.Services.Users;
using Common.Services.Mail;
using Common.Services.Management;
using Common.Services.Notifications;
using Common.Services.OwnLists;
using Common.Services.RestrictiveLists;
using Common.Services.ThirdPartyProfiling;
using Common.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Common.Services.Infrastructure.Services.Files;
using Common.Services.Infrastructure.Repositories.Files;

//using Common.Services.CompanyTypeList;
using FileShare = Common.Services.Infrastructure.Repositories.Files.FileShare;
using Common.Services.Infrastructure.Services.Queries;
using Common.Services.Queries;
using Common.Services.Infrastructure.Repositories.Queries;
using Common.DataAccess.EFCore.Repositories.RequestHelper;
using Common.Services.Infrastructure.Repositories.Relations_Countrys;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Common.Services.Infrastructure.Services.Lists;
using Common.Services.Infrastructure.Repositories.Lists;
using Common.DataAccess.EFCore.Repositories.Lists;

namespace Common.DIContainerCore
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, string connectionString, string connectionStringDocuments, string defaultConection)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<DataDocumentsContext>(options => options.UseSqlServer(connectionStringDocuments));
            services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();

            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IUserPhotoRepository, UserPhotoRepository>();
            services.AddTransient<IUserRepository<User>, UserRepository>();
            services.AddTransient<IUserManagementRepository<User>, UserManagementRepository>();
            services.AddTransient<IUserRoleRepository<UserRole>, UserRoleRepository>();
            services.AddTransient<IUserClaimRepository<UserClaim>, UserClaimRepository>();
            services.AddTransient<IUserClaimRepository<UserClaim>, UserClaimRepository>();
            services.AddTransient<IIdentityUserRepository<User>, IdentityUserRepository>();
            services.AddTransient<IRoleRepository<Role>, RoleRepository>();
            services.AddTransient<IUserRoleRepository<UserRole>, UserRoleRepository>();
            services.AddTransient<IUserClaimRepository<UserClaim>, UserClaimRepository>();
            services.AddTransient<IIndividualQueryRepository, IndividualQueryRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<INotificationSettingsRepository, NotificationSettingsRepository>();
            services.AddTransient<ICompanyTypeListRepository, CompanyTypeListRepository>();
            services.AddTransient<IThirdPartyTypeRepository, ThirdPartyTypeRepository>();
            services.AddTransient<IOwnListsTypesRepository, OwnListsTypeRepository>();
            services.AddTransient<IOwnListsRepository, OwnListsRepository>();

            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IUserService, UserService<User>>();

            services.AddTransient<IUserManagementService, UserManagementService<User>>();
            services.AddTransient<IUserManagementRepository<User>, UserManagementRepository>();

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<INotificationRepository<Notification>, NotificationRepository>();

            services.AddTransient<IEventRoiOperationTypeService, EventRoiOperationTypeService>();
            services.AddTransient<IEventRoiOperationTypeRepository<EventRoiOperationType>, EventRoiOperationTypeRepository>();

            services.AddTransient<IEventRoiOperationStatusService, EventRoiOperationStatusService>();
            services.AddTransient<IEventRoiOperationStatusRepository<EventRoiOperationStatus>, EventRoiOperationStatusRepository>();

            services.AddTransient<IEventRoiService, EventRoiService<Notification>>();
            services.AddTransient<IEventRoiRepository<EventRoi>, EventRoiRepository>();

            services.AddTransient<IContentService, ContentService<Content>>();

            services.AddTransient<IDocumentTypeRepository, DataAccess.EFCore.Repositories.DocumentTypeRepository>();
            services.AddTransient<Common.Services.Infrastructure.Services.IDocumentTypeService, DocumentTypeService>();

            services.AddTransient<IContentRepository<Content>, ContentRepository>();
            services.AddTransient<INotificationMonitoringRepository<Notification>, NotificationMonitoringRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IPlanRepository, PlanRepository>();
            services.AddTransient<IRiskProfileVariableRepository, RiskProfileVariableRepository>();
            services.AddTransient<ICategoryVariableRepository, CategoryVariableRepository>();
            services.AddTransient<IRiskProfileRepository, RiskProfileRepository>();
            services.AddTransient<INotificationMonitoringRepository<Notification>, NotificationMonitoringRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IListRepository, ListRepository>();
            services.AddTransient<IListTypeRepository, ListTypeRepository>();
            services.AddTransient<IListGroupRepository, ListGroupRepository>();
            services.AddTransient<ICountryRepository, DataAccess.EFCore.Repositories.Cities.CountryRepository>();
            services.AddTransient<IListPeriodicityRepository, ListPeriodicityRepository>();
            services.AddTransient<IPersonTypeRepository, PersonTypeRepository>();
            services.AddTransient<IBulkQueryRepository, BulkQueryRepository>();
            services.AddTransient<IBulkQueryAdditionalRepository, BulkQueryAdditionalRepository>();
            services.AddTransient<IBulkQueryAdditionalService, BulkQueryAdditionalService>();

            services.AddTransient<IPermissionsRepository, PermissionsRepository>();
            services.AddTransient<IPermissionsService, PermissionsService>();

            //services.AddTransient<INotificationSentRepository<NotificationsSent>, NotificationSentRepository>();


            services.AddTransient<IContentService, ContentService<Content>>();
            services.AddTransient<IContentRepository<Content>, ContentRepository>();
            services.AddTransient<IAccessService, AccessService<Access>>();
            services.AddTransient<IAccessRepository<Access>, AccessRepository>();
            services.AddTransient<IAccessSubModulesRepository, AccessSubModulesRepository>();
            services.AddTransient<IAccessSubModulesService, AccessSubModuleService>();

            services.AddTransient<IAdditionalServiceRepository, AdditionalServiceRepository>();
            services.AddTransient<IAdditionalCompanyServiceRepository, AdditionalCompanyServiceRepository>();
            services.AddTransient<IAdditionalCompanyServiceService, AdditionalCompanyServiceService>();

            services.AddTransient<Services.Infrastructure.Management.IDocumentTypeService, DocumentTypeService<DocumentType>>();
            services.AddTransient<IDocumentTypeRepository<DocumentType>, DataAccess.EFCore.Repositories.Management.DocumentTypeRepository>();
            services.AddTransient<IContentStatusesRepository, ContentStatusesRepository>();
            services.AddTransient<IStatesRepository, StatesRepository>();
            services.AddTransient<ICitiesRepository, CitiesRepository>();
            services.AddTransient<IThirdListRepository, ThirdListRepository>();



            #region Services
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IUserService, UserService<User>>();
            services.AddTransient<IUserManagementService, UserManagementService<User>>();
            services.AddTransient<IRiskProfileVariableRepository, RiskProfileVariableRepository>();
            services.AddTransient<ICategoryVariableRepository, CategoryVariableRepository>();
            services.AddTransient<IThirdPartyProfilingRepository, ThirdPartyProfilingRepository>();
            services.AddTransient<IParameterizeVariableService, ParameterizeVariableService>();
            services.AddTransient<IThirdPartyProfilingService, ThirdPartyProfilingService>();
            services.AddTransient<INotificationSettingsService, NotificationSettingsService>();
            services.AddTransient<IIndividualQueryService, IndividualQueryService>();
            services.AddTransient<ICompanyTypeListService, CompanyTypeListService>();
            services.AddTransient<IThirdPartyTypeService, ThirdPartyTypeService>();
            services.AddTransient<IOwnListTypesService, OwnListTypesService>();
            services.AddTransient<IOwnListsService, OwnListsService>();
            services.AddTransient<IEventRoiOperationTypeService, EventRoiOperationTypeService>();
            services.AddTransient<IEventRoiOperationStatusService, EventRoiOperationStatusService>();
            //services.AddTransient<IEventRoiService, EventRoiService<EventRoi>>();
            services.AddTransient<INotificationMonitoringService, NotificationMonitoringService>();
            services.AddTransient<INotificationMonitoringRepository<Notification>, NotificationMonitoringRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IPlanService, PlanService>();
            services.AddTransient<IEMailService, EMailService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IUserService, UserService<User>>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IUserManagementService, UserManagementService<User>>();
            services.AddTransient<IEventRoiOperationTypeService, EventRoiOperationTypeService>();
            services.AddTransient<IEventRoiOperationStatusService, EventRoiOperationStatusService>();
            services.AddTransient<IEventRoiService, EventRoiService<Notification>>();
            services.AddTransient<IListService, ListService>();
            services.AddTransient<IListTypeService, ListTypeService>();
            services.AddTransient<IListGroupService, ListGroupService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IListPeriodicityService, ListPeriodicityService>();
            services.AddTransient<IPersonTypeService, PersonTypeService>();
            services.AddTransient<Services.Infrastructure.Services.IDocumentTypeService, DocumentTypeService>();
            services.AddTransient<IBulkQueryService, BulkQueryService>();
            services.AddTransient<IContentTypeService, ContentTypeService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IBulkQueryAdditionalService, BulkQueryAdditionalService>();

            services.AddTransient<IContentTypeRepository, ContentTypeRepository>();

            services.AddTransient<IContentCategoryService, ContentCategoryService>();
            services.AddTransient<IContentCategoryRepository, ContentCategoryRepository>();
            services.AddTransient<IContentStatusesServices, ContentStatusesService>();
            services.AddTransient<IStatesService, StatesService>();
            services.AddTransient<ICitiesService, CitiesService>();
            services.AddTransient<IThirdListsService, ThirdListsService>();



            //services.AddTransient<IEventRoiService, EventRoiService<EventRoi>>();
            #endregion

            #region BD 

            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IReportRepository, ReportRepository>();

            services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ILogRepository, LogRepository>();
            #endregion

            services.AddScoped<IFileShare, FileShare>();

            services.AddScoped<RequestRepository>();
        }
    }
}
