WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// NHibernate & JsonSettings & DataAccess.
WsSqlContextManagerHelper.Instance.SetupJsonWebApp(builder.Environment.ContentRootPath, nameof(WsWebApiScales), false);
if (WsSqlCoreHelper.Instance.SessionFactory is null) throw new ArgumentException(
    $"{nameof(WsSqlCoreHelper.Instance.SessionFactory)}");
builder.Services.AddSingleton(WsSqlCoreHelper.Instance.SessionFactory);
builder.Services.AddScoped(_ => WsSqlCoreHelper.Instance.SessionFactory.OpenSession());

// POST XML from body.
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("string", MediaTypeHeaderValue.Parse("application/xml"));
}).AddXmlSerializerFormatters();

builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
}).AddXmlDataContractSerializerFormatters();

//builder.Services.AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//    });
//bool prettyPrintJson = builder.Configuration.GetValue<string>("PrettyPrintJsonOutput") == "true";
//builder.Services.AddControllers(options =>
//{
//    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
//    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

//    var jsonFormatter = options.OutputFormatters.OfType<SystemTextJsonOutputFormatter>().First();
//    jsonFormatter.SerializerOptions.WriteIndented = prettyPrintJson;
//});

// Swagger/OpenAPI https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Version = "v1",
        Title = "WEB API Scales Documentaion",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new("https://example.com/terms"),
        Contact = new()
        {
            Name = "Example Contact",
            Url = new("https://example.com/contact")
        },
        License = new()
        {
            Name = "Example License",
            Url = new("https://example.com/license")
        }
    });
});

WebApplication app = builder.Build();
// Swagger documentaion.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
