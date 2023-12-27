using Microsoft.AspNetCore.Mvc.Formatters;
using Ws.WebApiScales.Common.Services;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

SqlCoreHelper.Instance.SetSessionFactory(false);
if (SqlCoreHelper.Instance.SessionFactory is null)
    throw new ArgumentException($"{nameof(SqlCoreHelper.Instance.SessionFactory)}");

builder.Services.AddSingleton(SqlCoreHelper.Instance.SessionFactory);
builder.Services.AddScoped(_ => SqlCoreHelper.Instance.SessionFactory.OpenSession());

builder.Services.AddScoped<ResponseDto>();

builder.Services.AddScoped<IPluService, PluService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IPluCharacteristicService, PluCharacteristicService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
}).AddXmlDataContractSerializerFormatters();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllers();
app.Run();
