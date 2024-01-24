using Microsoft.AspNetCore.Mvc.Formatters;
using Ws.Domain.Services;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brand;
using Ws.WebApiScales.Features.Brand.Services;
using Ws.WebApiScales.Features.Nesting;
using Ws.WebApiScales.Features.Nesting.Services;
using Ws.WebApiScales.Features.Plu.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ResponseDto>();

#region Ready

builder.Services.AddScoped<IBoxApiService, BoxApiService>();

#endregion


builder.Services.AddScoped<IPluApiService, PluApiService>();
builder.Services.AddScoped<IBrandApiService, BrandApiService>();
builder.Services.AddScoped<IPluCharacteristicApiService, PluCharacteristicApiService>();

builder.Services.AddDomainServices();

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
