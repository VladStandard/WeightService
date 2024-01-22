using Microsoft.AspNetCore.Mvc.Formatters;
using Ws.Services;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brand;
using Ws.WebApiScales.Features.Nesting;
using Ws.WebApiScales.Features.Plu;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ResponseDto>();
builder.Services.AddScoped<IPluApiService, PluApiService>();
builder.Services.AddScoped<IBrandApiService, BrandApiService>();
builder.Services.AddScoped<IPluCharacteristicApiService, PluCharacteristicApiService>();
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
