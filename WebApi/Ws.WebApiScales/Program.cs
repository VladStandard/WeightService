using Microsoft.AspNetCore.Mvc.Formatters;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Services;
using Ws.WebApiScales.Settings;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

SqlCoreHelper.Instance.SetSessionFactory(false);
if (SqlCoreHelper.Instance.SessionFactory is null)
    throw new ArgumentException($"{nameof(SqlCoreHelper.Instance.SessionFactory)}");

builder.Services.AddSingleton(SqlCoreHelper.Instance.SessionFactory);
builder.Services.AddScoped(_ => SqlCoreHelper.Instance.SessionFactory.OpenSession());

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
