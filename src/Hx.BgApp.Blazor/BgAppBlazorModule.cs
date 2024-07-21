using System;
using System.IO;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Hx.BgApp.Blazor.Menus;
using Hx.BgApp.EntityFrameworkCore;
using Hx.BgApp.Localization;
using Hx.BgApp.MultiTenancy;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme;
using Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Blazor.Server;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor.Server;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Blazor.Server;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Settings;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Volo.Abp.Json;
using System.Collections.Generic;

namespace Hx.BgApp.Blazor;

[DependsOn(
    typeof(BgAppApplicationModule),
    typeof(BgAppEntityFrameworkCoreModule),
    typeof(BgAppHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreComponentsServerLeptonXLiteThemeModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpIdentityBlazorServerModule),
    typeof(AbpTenantManagementBlazorServerModule),
    typeof(AbpSettingManagementBlazorServerModule)
   )]
public class BgAppBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(BgAppResource),
                typeof(BgAppDomainModule).Assembly,
                typeof(BgAppDomainSharedModule).Assembly,
                typeof(BgAppApplicationModule).Assembly,
                typeof(BgAppApplicationContractsModule).Assembly,
                typeof(BgAppBlazorModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("BgApp");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        context.Services.AddCors(options =>
        {
            options.AddPolicy("Default",
            builder =>
            {
                builder.WithOrigins(configuration["CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray())
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        Configure<AbpJsonOptions>(options =>
        {
            options.InputDateTimeFormats =new List<string>() { "yyyy-MM-dd HH:mm:ss" };
        });
        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureSwaggerServices(context.Services);
        ConfigureAutoApiControllers();
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMenu(context);
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        context.Services.AddOpenIddict().AddServer(
            Options =>
            {
                Options.UseAspNetCore().DisableTransportSecurityRequirement();
            }
        );
        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidateIgnoredHttpMethods.Add("POST");
            options.AutoValidateIgnoredHttpMethods.Add("DELETE");
            options.AutoValidateIgnoredHttpMethods.Add("PUT");
            options.AutoValidateIgnoredHttpMethods.Add("GET");
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            // MVC UI
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );

            //BLAZOR UI
            options.StyleBundles.Configure(
                BlazorLeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/blazor-global-styles.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/Hx.BgApp.Blazor.styles.css");
                }
            );
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<BgAppDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Hx.BgApp.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<BgAppDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Hx.BgApp.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<BgAppApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Hx.BgApp.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<BgAppApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Hx.BgApp.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<BgAppBlazorModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BgApp API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BgAppMenuContributor());
        });
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(BgAppBlazorModule).Assembly;
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers
                .Create(typeof(BgAppBlazorModule).Assembly, opts =>
                {
                    opts.RootPath = "app";
                })
                .Create(typeof(BgAppApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "app";
                })
                .Create(typeof(BgAppHttpApiModule).Assembly, opts =>
                {
                    opts.RootPath = "app";
                })
                .Create(typeof(AbpIdentityHttpApiModule).Assembly, opts =>
                {
                    opts.RootPath = "app";
                })
                .Create(typeof(AbpIdentityApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "app";
                });
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BgAppBlazorModule>();
        });
    }

    public override async void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();
        var configuration = context.GetConfiguration();
        var localSetting = app.ApplicationServices.GetService<SettingDefinitionManager>();
        if (localSetting != null)
        {
            //localSetting.Get(LocalizationSettingNames.DefaultLanguage).DefaultValue = "zh-Hans";
            (await localSetting.GetAsync(LocalizationSettingNames.DefaultLanguage)).DefaultValue = "zh-Hans";
        }
        app.UseAbpRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseCors("Default");
        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        string? root = configuration[AppGlobalProperties.ServerBaseUrl];
        if (root != null)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(root),
                RequestPath = "/cmsfile",
                OnPrepareResponse = (c) =>
                {
                    c.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                }
            });
        }
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "BgApp API");
        });
        app.UseConfiguredEndpoints();
    }
}