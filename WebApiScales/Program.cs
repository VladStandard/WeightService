// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Net.Http.Headers;
using NHibernate;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});

ISessionFactory GetSessionFactory(string connectionString)
{
    FluentConfiguration configuration = Fluently
        .Configure()
        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString)
            .ShowSql()
            .Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>()
        );
    configuration.ExposeConfiguration(x => x.SetProperty("hbm2ddl.keywords", "auto-quote"));
    return configuration.BuildSessionFactory();
}
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"builder.Configuration: {builder.Configuration}");
Console.WriteLine($"connectionString: {connectionString}");
ISessionFactory sessionFactory = GetSessionFactory(connectionString);
builder.Services.AddSingleton(sessionFactory);
builder.Services.AddScoped(factory => sessionFactory.OpenSession());

// Allow body
//builder.Services.AddMvc(options =>
//{
//    //options.InputFormatters.Clear();
//    //options.OutputFormatters.Clear();

//    // Since both XmlSerializer and DataContractSerializer based formatters
//    // have supported media types of 'application/xml' and 'text/xml',  it 
//    // would be difficult for a test to choose a particular formatter based on
//    // request information (Ex: Accept header).
//    // So here we instead clear out the default supported media types and create new
//    // ones which are distinguishable between formatters.
//    XmlSerializerInputFormatter xmlSerializerInputFormatter = new XmlSerializerInputFormatter(options);
//    xmlSerializerInputFormatter.SupportedMediaTypes.Clear();
//    xmlSerializerInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml-xmlser"));
//    xmlSerializerInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml-xmlser"));
//    xmlSerializerInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
//    xmlSerializerInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));

//    XmlSerializerOutputFormatter xmlSerializerOutputFormatter = new XmlSerializerOutputFormatter();
//    xmlSerializerOutputFormatter.SupportedMediaTypes.Clear();
//    xmlSerializerOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml-xmlser"));
//    xmlSerializerOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml-xmlser"));
//    xmlSerializerOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
//    xmlSerializerOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));

//    XmlDataContractSerializerInputFormatter dcsInputFormatter = new XmlDataContractSerializerInputFormatter(options);
//    dcsInputFormatter.SupportedMediaTypes.Clear();
//    dcsInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml-dcs"));
//    dcsInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml-dcs"));
//    dcsInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
//    dcsInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));

//    XmlDataContractSerializerOutputFormatter dcsOutputFormatter = new XmlDataContractSerializerOutputFormatter();
//    dcsOutputFormatter.SupportedMediaTypes.Clear();
//    dcsOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml-dcs"));
//    dcsOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml-dcs"));
//    dcsOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
//    dcsOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));

//    options.InputFormatters.Add(dcsInputFormatter);
//    options.InputFormatters.Add(xmlSerializerInputFormatter);
//    options.OutputFormatters.Add(dcsOutputFormatter);
//    options.OutputFormatters.Add(xmlSerializerOutputFormatter);

//    xmlSerializerInputFormatter.WrapperProviderFactories.Add(new BarcodeBottomWrapperProviderFactory());
//    xmlSerializerOutputFormatter.WrapperProviderFactories.Add(new BarcodeBottomWrapperProviderFactory());
//    dcsInputFormatter.WrapperProviderFactories.Add(new BarcodeBottomWrapperProviderFactory());
//    dcsOutputFormatter.WrapperProviderFactories.Add(new BarcodeBottomWrapperProviderFactory());

//    //options.RespectBrowserAcceptHeader = true;
//    //options.AllowEmptyInputInBodyModelBinding = true;
//    ////foreach (IInputFormatter formatter in options.InputFormatters)
//    ////{
//    ////    if (formatter.GetType() == typeof(SystemTextJsonInputFormatter))
//    ////        ((SystemTextJsonInputFormatter)formatter).SupportedMediaTypes.Add(
//    ////            Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain"));
//    ////}
//    //options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter(options));
//    //options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
//}).AddXmlSerializerFormatters();

builder.Services.AddMvc(options =>
{
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    //options.FormatterMappings.SetMediaTypeMappingForFormat("config", MediaTypeHeaderValue.Parse("application/xml"));
    //options.FormatterMappings.SetMediaTypeMappingForFormat("js", MediaTypeHeaderValue.Parse("application/json"));
}).AddXmlSerializerFormatters();

builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.OutputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
//}).AddXmlDataContractSerializerFormatters();

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

WebApplication app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
