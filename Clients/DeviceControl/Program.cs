// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddOptions();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

#endregion

#region AddScoped

builder.Services.AddScoped<WsUserService>();
builder.Services.AddScoped<WsRouteService>();
builder.Services.AddScoped<WsLocalStorageService>();
builder.Services.AddScoped<WsThemeService>();
builder.Services.AddScoped<WsJsService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IWsUserRightsService, WsUserRightsService>();
builder.Services.AddScoped<AuthenticationStateProvider, WsCustomAuthStateProvider>();

#endregion

#region App

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

WsSqlContextManagerHelper.Instance.SetupJsonWebApp(
    app.Environment.ContentRootPath,
    null,
    true
);

try
{
    // Лог памяти.
    new WsSqlLogMemoryRepository().Save(app.Environment.ApplicationName);
    app.Run();
}
catch (Exception ex)
{
    WsFileLoggerHelper.Instance.StoreException(ex);
}

#endregion