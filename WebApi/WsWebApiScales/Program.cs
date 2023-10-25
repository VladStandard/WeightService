using Microsoft.AspNetCore.Mvc.Formatters;
using WsWebApiCore.Settings;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Services;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WsSqlContextManagerHelper.Instance.SetupJsonWebApp(builder.Environment.ContentRootPath, nameof(WsWebApiScales), false);
if (WsSqlCoreHelper.Instance.SessionFactory is null)
    throw new ArgumentException($"{nameof(WsSqlCoreHelper.Instance.SessionFactory)}");

builder.Services.AddSingleton(WsSqlCoreHelper.Instance.SessionFactory);
builder.Services.AddScoped(_ => WsSqlCoreHelper.Instance.SessionFactory.OpenSession());

builder.Services.AddTransient<ResponseDto>();

builder.Services.AddTransient<PluService>();
builder.Services.AddTransient<BrandService>();
builder.Services.AddTransient<PluCharacteristicService>();
builder.Services.AddHttpContextAccessor();

// POST XML from body.
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
    options.OutputFormatters.Add(new XmlSerializerOutputFormatterNamespace());
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
}).AddXmlSerializerFormatters();


builder.Services.AddControllers(options => {
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});



WebApplication app = builder.Build();


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllers();
app.Run();
