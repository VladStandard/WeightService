// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiTerra1000;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    private ISessionFactory GetSessionFactory(string connectionString)
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

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = Configuration.GetConnectionString("DefaultConnection");
        ISessionFactory sessionFactory = GetSessionFactory(connectionString);

        services.AddSingleton(sessionFactory);
        services.AddScoped(factory => sessionFactory.OpenSession());

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "terra", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "terra v1"));
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
