// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using BlazorDeviceControl.Service;
using BlazorDownloadFile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Radzen;
using RimDev.Stuntman.Core;
using System;
using System.IO;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace BlazorDeviceControl
{
    public class Startup
    {
        #region Public and private fields and properties

        public IConfiguration Configuration { get; }
        public AppSettingsEntity AppSettings { get; private set; } = AppSettingsEntity.Instance;
        public static readonly StuntmanOptions StuntmanOptions = new();
        private string _contentRootPath;
        private string _webRootPath;
        private string _projectRootPath;

        #endregion

        #region Public and private methods

        //public Startup(IConfiguration configuration)
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;
            _webRootPath = env.WebRootPath;
            _projectRootPath = AppContext.BaseDirectory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            // Inject.
            services.AddHotKeys();
            services.AddSingleton<JsonSettingsEntity>();
            services.AddScoped<ContextMenuService>();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            //services.AddScoped<AuthEntity>();
            //services.AddScoped<>(AuthenticationStateProvider, );
            services.AddMudServices();

            //// Authentication.
            //services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
            //// Windows authentication may not be applied with Kestrel without this line
            //services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);
            //services.AddAuthentication();
            //services.AddAuthorization();
            //services.AddAuthorizationCore();

            // Authentication.
            services.AddAuthentication();
            services.AddAuthorization();

            // Other.
            services.AddControllersWithViews();
            services.AddBlazorDownloadFile();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddScoped<IFileDownload, FileDownload>();

            services.AddOptions();
            //services.AddAuthorizationCore();
            //services.AddHttpContextAccessor();

            SetupStuntman(services);
        }

        private void SetupStuntman(IServiceCollection services)
        {
            foreach (StuntmanUser user in AppSettings.StuntmanUsers.Users)
            {
                StuntmanOptions.AddUser(user);
            }

            // You can also add users using HTTP/HTTPS or the file system!
            //string fileUsers = _contentRootPath + @"\local-users.json";
            //if (File.Exists(fileUsers))
            //    _stuntmanOptions.AddUsersFromJson(fileUsers);
            ////_stuntmanOptions.AddUsersFromJson("https://example.com/web-test-users.json");

            // Optional alignment of user picker
            // Supported options are:
            // - StuntmanAlignment.Left (default)
            // - StuntmanAlignment.Center
            // - StuntmanAlignment.Right
            StuntmanOptions.SetUserPickerAlignment(StuntmanAlignment.Right);

            // Only show when debug is true in Web.config.
            //if (System.Web.HttpContext.Current.IsDebuggingEnabled)
            //{
            //    app.UseStuntman(StuntmanOptions);
            //}

            StuntmanOptions.EnableServer();
            services.AddStuntman(StuntmanOptions);
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
            // Authentication.
            app.UseAuthentication();
            app.UseAuthorization();
            // Last step.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        #endregion
    }
}
