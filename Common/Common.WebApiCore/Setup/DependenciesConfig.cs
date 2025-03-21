/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.WebApiCore.Identity;
using Common.DIContainerCore;
using Common.Entities;
using Common.Services.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Common.Utils;

namespace Common.WebApiCore.Setup
{
    public class DependenciesConfig
    {
        public static void ConfigureDependencies(IServiceCollection services, string connectionString, string connectionStringDocuments, string defaultConection)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentContextProvider, CurrentContextProvider>();

            services.AddSingleton<JwtManage>();

            ContainerExtension.Initialize(services, connectionString, connectionStringDocuments, defaultConection);

            services.AddTransient<IAuthenticationService, AuthenticationService<User>>();
            services.AddTransient<IRoleService, RoleService<User, Role>>();
            //services.AddTransient<INotificationSettingsService, NotificationSettingsService<>>();
        }
    }
}
