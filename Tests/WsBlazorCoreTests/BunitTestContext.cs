// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using WsBlazorCore.Services;

namespace WsBlazorCoreTests;

public abstract class BunitTestContext : TestContextWrapper
{
    [SetUp]
    public void Setup()
    {
        TestContext = new();
        TestContext.Services.AddScoped<Radzen.DialogService>();
        TestContext.Services.AddScoped<NotificationService>();
        TestContext.Services.AddScoped<TooltipService>();
        TestContext.Services.AddScoped<ContextMenuService>();
        //TestContext.Services.AddMudServices();
        // Authentication & Authorization.
        //TestContext.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
        //TestContext.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        // Windows authentication may not be applied with Kestrel without this line
        TestContext.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);
        TestContext.Services.AddAuthentication();
        TestContext.Services.AddAuthorization();
        TestContext.Services.AddHttpContextAccessor();
        // Other.
        TestContext.Services.AddControllersWithViews();
        //TestContext.Services.AddBlazorDownloadFile();
        TestContext.Services.AddScoped<IFileUpload, FileUpload>();
        TestContext.Services.AddScoped<IFileDownload, FileDownload>();
        //
        TestContext.Services.AddOptions();
        TestContext.Services.AddAuthorizationCore();
        TestContext.Services.AddHttpClient();

        FakeWebAssemblyHostEnvironment hostEnvironment = TestContext.Services.GetRequiredService<FakeWebAssemblyHostEnvironment>();
        // Sets the environment to "Development". There are two other helper 
        // methods available as well, SetEnvironmentToProduction() and 
        // set SetEnvironmentToStaging(), or environment can also be changed
        // directly through the hostEnvironment.Environment property.
        hostEnvironment.SetEnvironmentToDevelopment();
        // Assert - inspects markup to verify the message
        //cut.Find("#message").MarkupMatches($"<p>Hello Developers.</p>");

    }

    [TearDown]
    public void TearDown() => TestContext?.Dispose();
}
