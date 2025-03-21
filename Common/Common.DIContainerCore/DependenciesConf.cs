using Common.DIContainerCore;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Management;
using Common.Services.Infrastructure.Services;
using Common.Services.Management;
using Microsoft.Extensions.DependencyInjection;


namespace Common.Utils.Setup
{
    public class DependenciesConf
    {
        public static void ConfigureDependencies(IServiceCollection services, string connectionString, string connectionStringDocuments, string defaultConection)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentContextProvider, CurrentContextProvider>();

            services.AddSingleton<JwtManage>();

            ContainerExtension.Initialize(services, connectionString, connectionStringDocuments, defaultConection);

            services.AddTransient<IAuthService, AuthService<User>>();
            services.AddScoped<ISendEmail, SendEmail>();
            services.AddTransient<IRoleService, RoleService<User, Role>>();
            //services.AddTransient<INotificationSettingsService, NotificationSettingsService<>>();
        }
    }
}
