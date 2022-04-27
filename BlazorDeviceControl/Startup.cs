// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using BlazorDeviceControl.Service;
using BlazorDownloadFile;
using DataCore.Protocols;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Radzen;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace BlazorDeviceControl
{
    public class Startup
    {
        #region Public and private fields and properties

        public AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;
        public IConfiguration Configuration { get; }

        #endregion

        #region Public and private methods

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            // Inject.
            services.AddHotKeys();
            //services.AddSingleton<JsonSettingsBase>();
            services.AddScoped<ContextMenuService>();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddMudServices();

            // Authentication & Authorization.
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
            // Windows authentication may not be applied with Kestrel without this line
            services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddHttpContextAccessor();
            //services.AddAuthorizationCore();

            // Other.
            services.AddControllersWithViews();
            services.AddBlazorDownloadFile();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddScoped<IFileDownload, FileDownload>();

            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Authentication & Authorization after app.UseRouting().
            app.UseAuthentication();
            app.UseAuthorization();

            // Last step.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            AppSettings.DataAccess.Json.SetupForBlazorApp(env.ContentRootPath, 
                NetUtils.GetLocalHostName(false), nameof(BlazorDeviceControl));
        }

        #endregion
    }
}
