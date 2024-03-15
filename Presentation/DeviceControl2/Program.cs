using Blazorise;
using Blazorise.Icons.FontAwesome;
using DeviceControl2.Source.App;
using DeviceControl2.Source.Shared.Auth;
using DeviceControl2.Source.Shared.Services;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddBlazorise().AddEmptyProviders().AddFontAwesomeIcons();
builder.Services.AddFluentUIComponents();
builder.Services.AddDomainServices();
builder.Services.AddLocalization();

builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<StartupService>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
