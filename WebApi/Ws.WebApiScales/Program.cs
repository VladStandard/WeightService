using Microsoft.AspNetCore.Mvc.Formatters;
using Ws.Services;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brand;
using Ws.WebApiScales.Features.Nesting;
using Ws.WebApiScales.Features.Plu;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ResponseDto>();
builder.Services.AddScoped<IPluService, PluService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IPluCharacteristicService, PluCharacteristicService>();
builder.Services.AddVsServices();

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
