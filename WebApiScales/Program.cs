// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Authentication.Negotiate;
using NHibernate;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

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
