/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using AutoMapper.Configuration;
using Common.Services.Infrastructure.MappingProfiles;
using Common.Services.Infrastructure.MappingProfiles.Report;
using Common.Services.Infrastructure.MappingProfiles.RestrictiveLists;
using Common.Services.Infrastructure.MappingProfiles.ThirdPartyProfiling;

namespace Common.WebApiCore.Setup
{
    public static class AutoMapperConfig
    {
        public static void Configure(MapperConfigurationExpression config)
        {
            config.AllowNullCollections = false;

            config.AddProfile<UserProfile>();
            config.AddProfile<SettingsProfile>();
            config.AddProfile<NotificationSettingsProfile>();
            config.AddProfile<UserManagementProfile>();
            // config.AddProfile<NotificationsSentProfile>();
            config.AddProfile<ListProfile>();
            config.AddProfile<QueryListProfile>(); 
            config.AddProfile<DocumentProfile>(); 
            config.AddProfile<NotificationProfile>();
            config.AddProfile<EventRoiProfile>();
            config.AddProfile<ThirdPartyTypeProfile>();
            config.AddProfile<ContentProfile>();
            config.AddProfile<RiskProfileVariableProfile>();
            config.AddProfile<CategoryVariableProfile>();
            config.AddProfile<RiskProfileProfile>();
            
            config.AddProfile<QueryProfile>();
            config.AddProfile<QueryDetailProfile>();

            config.AddProfile<ListGroupProfile>();
            config.AddProfile<ListTypeProfile>();
            config.AddProfile<CompanyTypeListProfile>();
            config.AddProfile<BulkQueryServicesAdditionalProfile>();
        }
    }
}
