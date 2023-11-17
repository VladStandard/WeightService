using Microsoft.AspNetCore.Authentication.Negotiate;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddOptions();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

#endregion

#region AddScoped

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RouteService>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<JsService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IUserRightsService, UserRightsService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

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

SqlCoreHelper.Instance.SetSessionFactory(false);

try
{
    app.Run();
}
catch (Exception ex)
{
    FileLoggerHelper.Instance.StoreException(ex);
}

#endregion
