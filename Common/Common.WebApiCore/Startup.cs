/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Text;
using AutoMapper;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.MappingProfiles;
using Common.Services.Infrastructure.MappingProfiles.Report;
using Common.Services.Infrastructure.MappingProfiles.RestrictiveLists;
using Common.Services.Infrastructure.MappingProfiles.ThirdPartyProfiling;
using Common.WebApiCore.Identity;
using Common.WebApiCore.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Common.Services.Infrastructure.MappingProfiles.Queries;
using Common.Utils.Setup;
using Common.Utils;
using Mapper = AutoMapper.Mapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using Common.Services.Infrastructure.MappingProfiles.Cities;
using Common.Services.Infrastructure.MappingProfiles.Lists;
using Common.Entities.Relations_Countrys;
using Common.WebApiCore.Middlewares;
using Common.Services.Infrastructure.MappingProfiles.IndividualQueryExternal;

//using AutoMapper.Configuration;

namespace Common.WebApiCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        protected void ConfigureDependencies(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var connectionString = Configuration.GetConnectionString("localDb");
            var connectionStringDocuments = Configuration.GetConnectionString("documentsDb");
            var defaultConection = Configuration.GetConnectionString("default");
            DependenciesConf.ConfigureDependencies(services, connectionString, connectionStringDocuments, defaultConection);
        }

        protected void ConfigureIdentity(IServiceCollection services)
        {
            IdentityConfig.Configure(services);
        }



        public virtual void ConfigureServices(IServiceCollection services)
        {
            ConfigureIdentity(services);
            services.ConfigureAuth(Configuration);

            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = false;
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<SettingsProfile>();
                cfg.AddProfile<NotificationSettingsProfile>();
                cfg.AddProfile<CompanyTypeListProfile>();
                cfg.AddProfile<ThirdPartyTypeProfile>();
                cfg.AddProfile<UserManagementProfile>();
                //cfg.AddProfile<NotificationsSentProfile>();
                //cfg.AddProfile<EventProfile>();
                cfg.AddProfile<QueryListProfile>();
                cfg.AddProfile<DocumentProfile>();
                cfg.AddProfile<NotificationProfile>();
                cfg.AddProfile<EventRoiProfile>();
                cfg.AddProfile<ContentProfile>();

                cfg.AddProfile<RiskProfileVariableProfile>();
                cfg.AddProfile<CategoryVariableProfile>();
                cfg.AddProfile<RiskProfileProfile>();

                cfg.AddProfile<NotificationsMonitoringProfile>();
                cfg.AddProfile<CompanyProfile>();
                cfg.AddProfile<OwnListsProfile>();
                cfg.AddProfile<OwnListsTypeProfile>();
                cfg.AddProfile<OwnListsResponseProfile>();
                cfg.AddProfile<PlanProfile>();
                cfg.AddProfile<ListProfile>();
                cfg.AddProfile<ListGroupProfile>();
                cfg.AddProfile<ListTypeProfile>();
                cfg.AddProfile<PeriodicityProfile>();
                cfg.AddProfile<ListBulkQueryProfile>();
                cfg.AddProfile<PermissionsProfile>();
                cfg.AddProfile<CountryProfile>();
                cfg.AddProfile<QueryProfile>();
                cfg.AddProfile<QueryDetailProfile>();
                cfg.AddProfile<AccessProfile>();
                cfg.AddProfile<DocumentTypeProfile>();
                cfg.AddProfile<BulkQueryServicesAdditionalProfile>();
                cfg.AddProfile<BulkQueryListExcelProfile>();
                cfg.AddProfile<BulkQueryListExcelCoincidence>();
                cfg.AddProfile<BulkQueryOwnListExcelProfile>();
                cfg.AddProfile<StateProfile>();
                cfg.AddProfile<CitiesProfile>();
                cfg.AddProfile<IndividualQueryExternalResponseEsProfile>();
                cfg.AddProfile<ListExternalEsProfile>();
                cfg.AddProfile<OwnListResponseEsProfile>();
		        cfg.AddProfile<ThirdListsProfile>();                
		        cfg.AddProfile<IndividualQueryExternalParamsEsProfile>();
		        cfg.AddProfile<BulkQueryServiceAdditionalDataExcelProfile>();
            });

            services.AddAutoMapper(typeof(Startup).Assembly);

            var settings = new EmailSettings();
            Configuration.GetSection("EmailConfig").Bind(settings);
            services.AddSingleton(settings);

            services.AddMvc();
            //AutoMapperConfig.Configure(new AutoMapper.Configuration.MapperConfigurationExpression());

            /* var mapperConfig = new MapperConfiguration(mc =>
             {
                 mc.AllowNullCollections = false;
                 mc.AddProfile<UserProfile>();
                 mc.AddProfile<SettingsProfile>();
                 mc.AddProfile<NotificationSettingsProfile>();
             });
             IMapper mapper = mapperConfig.CreateMapper();
             services.AddSingleton(mapper);*/

            ConfigureDependencies(services);
            services.ConfigureSwagger();

            services.ConfigureCors();

            services.AddAuthorization(opt => opt.RegisterPolicies());
            services
                .AddControllers(opt =>
                {
                    opt.UseCentralRoutePrefix(new RouteAttribute("api"));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddHttpClient();
            services.AddSingleton<HttpClientFactory>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env, IDataBaseInitializer dataBaseInitializer)
        {
            if (dataBaseInitializer != null)
            {
                dataBaseInitializer.Initialize();
            }
            else
            {
                // TODO: add logging
            }

            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            //RotativaConfiguration.Setup(env.ContentRootPath);
            // RotativaConfiguration.Setup(@"C:\Users\usuario\source\repos\InspektorBackend\Common\Common.WebApiCore\", "Files");
            //RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env);
            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseConfiguredSwagger();

            app.UseMiddleware<SwaggerBasicAuthMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //        var exception = exceptionHandlerPathFeature.Error;
            //        var logger = context.RequestServices.GetService<ILogger<Startup>>();
            //        logger.LogError(exception, "Error no controlado en la solicitud: {Path}", context.Request.Path);

            //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        await context.Response.WriteAsync("Ocurrió un error en el servidor. Por favor, inténtalo de nuevo más tarde.");
            //    });
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
